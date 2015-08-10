using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Základ pohled pro filtrační pole, který zná definici svého fieldu.
    /// </summary>
    /// <typeparam name="T">Typ, do kterého se má pohled vykreslovat.</typeparam>
    public abstract class FieldView<T> : IFieldView<T>
    {
        /// <summary>
        /// Definice fieldu, ke kterému se pohled váže.
        /// </summary>
        protected IFieldDefinition FieldDefinition { get; private set; }

        public FieldView(IFieldDefinition fieldDefinition)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            FieldDefinition = fieldDefinition;
        }

        public abstract bool TryGetValue(out object value);

        public abstract bool TrySetValue(object value);

        public abstract void Render(T target);
    }

    /// <summary>
    /// Pohled pro filtrační pole, který se váže na konkrétní typ hodnot, se kterémi umí pracovat.
    /// Zpracovává hodnotu, které byla nastavena do pohledu ještě před jeho vykreslením a předává ji jako <code>defaultValue</code> do <see cref="RenderInternal"/>.
    /// V metodách <see cref="SetData"/> a <see cref="GetData"/> aplikuje <see cref="IValueConverter"/>, pokud je k dispozici.
    /// </summary>
    /// <typeparam name="TValue">Typ hodnoty.</typeparam>
    /// <typeparam name="T">Typ kontrolu.</typeparam>
    public abstract class FieldView<TValue, T> : FieldView<T>
    {
        /// <summary>
        /// Příznak, zda se má automaticky aplikovat <see cref="IFieldMetadata.ValueConverter"/>.
        /// </summary>
        protected bool CanUseConveter { get; set; }

        /// <summary>
        /// Výchozí hodnota, která byla do pohledu nastavena (typicky před jeho vykreslením).
        /// </summary>
        protected TValue DefaultValue { get; private set; }

        /// <summary>
        /// Zda je pohled již vykreslen.
        /// </summary>
        protected bool IsRendered { get; private set; }

        public FieldView(IFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        {
            CanUseConveter = true;

            TValue defaultValue;
            if (FieldDefinition.Metadata.TryGet("DefaultValue", out defaultValue))
                DefaultValue = defaultValue;
            else
                DefaultValue = default(TValue);
        }

        /// <summary>
        /// Řeší vlastní získání hodnoty z pohledu, po jeho vykreslení.
        /// </summary>
        /// <param name="value">Hodnotu získanou z pohledu.</param>
        /// <returns>Zda bylo možné hodnotu získat.</returns>
        protected abstract bool TryGetValueInternal(out TValue value);

        public override bool TryGetValue(out object value)
        {
            if (!IsRendered)
            {
                value = DefaultValue;
                return true;
            }

            TValue targetValue;
            if (TryGetValueInternal(out targetValue))
            {
                //if (Metadata.ValueConverter != null)
                //{
                //    return Metadata.ValueConverter.ConvertFrom(value);
                //}

                value = targetValue;
                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Řeší vlastní nastavení hodnoty do pohledu, po jeho vykreslení.
        /// </summary>
        /// <param name="value">Nová hodnota.</param>
        /// <returns>Zda bylo možné hodnotu nastavit.</returns>
        protected abstract bool TrySetValueInternal(TValue value);

        public override bool TrySetValue(object value)
        {
            if (value == null)
            {
                value = default(TValue);
            }
            else if (!(value is TValue))
            {
                //if (Metadata.ValueConverter != null)
                //{
                //    value = Metadata.ValueConverter.Convert(value, typeof(TValue));
                //    //TODO: Check again if (!(value is TValue))? What about null value?
                //}
                //else if (value != null)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "Missing conveter, unnable to convert value of type '{0}' to required type '{1}'.",
                            value.GetType(),
                            typeof(TValue)
                        )
                    );
                }
            }


            if (!IsRendered)
                DefaultValue = (TValue)value;
            else
                return TrySetValueInternal((TValue)value);

            return true;
        }

        /// <summary>
        /// Řeší vlastní vykreslení pohledu.
        /// </summary>
        /// <param name="target">Cíl, kam se má pohled vykreslit.</param>
        /// <param name="defaultValue">Výchozí hodnota pohledu.</param>
        protected abstract void RenderInternal(T target, TValue defaultValue);

        public override sealed void Render(T target)
        {
            if (!IsRendered)
            {
                RenderInternal(target, DefaultValue);
                IsRendered = true;
            }
        }
    }
}