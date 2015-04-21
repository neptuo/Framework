/* Generated by SharpKit 5 v5.4.4 */
if (typeof($CreateException)=='undefined') 
{
    var $CreateException = function(ex, error) 
    {
        if(error==null)
            error = new Error();
        if(ex==null)
            ex = new System.Exception.ctor();       
        error.message = ex.message;
        for (var p in ex)
           error[p] = ex[p];
        return error;
    }
}


if (typeof(JsTypes) == "undefined")
    var JsTypes = [];
var Neptuo$PresentationModels$TypeModels$Expressions$FieldValueProviderCollection$1 = {
    fullname: "Neptuo.PresentationModels.TypeModels.Expressions.FieldValueProviderCollection$1",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    interfaceNames: ["Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProviderCollection$1"],
    Kind: "Class",
    definition: {
        ctor: function (TModel){
            this.TModel = TModel;
            this.storage = new System.Collections.Generic.Dictionary$2.ctor(System.String.ctor, Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProvider$1.ctor);
            System.Object.ctor.call(this);
        },
        Add: function (fieldIdentifier, provider){
            Neptuo.Ensure.NotNullOrEmpty(fieldIdentifier, "fieldIdentifier");
            Neptuo.Ensure.NotNull$$Object$$String(provider, "provider");
            this.storage.set_Item$$TKey(fieldIdentifier, provider);
            return this;
        },
        TryGet: function (fieldIdentifier, provider){
            Neptuo.Ensure.NotNullOrEmpty(fieldIdentifier, "fieldIdentifier");
            return this.storage.TryGetValue(fieldIdentifier, provider);
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$Expressions$FieldValueProviderCollection$1);
var Neptuo$PresentationModels$TypeModels$Expressions$ExpressionModelValueProvider$1 = {
    fullname: "Neptuo.PresentationModels.TypeModels.Expressions.ExpressionModelValueProvider$1",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    interfaceNames: ["Neptuo.PresentationModels.IModelValueProvider"],
    Kind: "Class",
    definition: {
        ctor: function (TModel, model, valueProviders){
            this.TModel = TModel;
            this._Model = null;
            this._ValueProviders = null;
            System.Object.ctor.call(this);
            Neptuo.Ensure.NotNull$$Object$$String(model, "model");
            Neptuo.Ensure.NotNull$$Object$$String(valueProviders, "valueProviders");
            this.set_Model(model);
            this.set_ValueProviders(valueProviders);
        },
        Model$$: "`0",
        get_Model: function (){
            return this._Model;
        },
        set_Model: function (value){
            this._Model = value;
        },
        ValueProviders$$: "Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProviderCollection`1[[`0]]",
        get_ValueProviders: function (){
            return this._ValueProviders;
        },
        set_ValueProviders: function (value){
            this._ValueProviders = value;
        },
        TryGetValue: function (identifier, value){
            value.Value = this.GetValueProvider(identifier).GetValue(this.get_Model());
            return true;
        },
        TrySetValue: function (identifier, value){
            this.GetValueProvider(identifier).SetValue(this.get_Model(), value);
            return true;
        },
        GetValueProvider: function (identifier){
            var valueProvider;
            if (!(function (){
                var $1 = {
                    Value: valueProvider
                };
                var $res = this.get_ValueProviders().TryGet(identifier, $1);
                valueProvider = $1.Value;
                return $res;
            }).call(this))
                throw $CreateException(Neptuo._EnsureArgumentExtensions.ArgumentOutOfRange(Neptuo.Ensure.Exception, "identifier", "Unnable to find property \'{0}\'.", identifier), new Error());
            return valueProvider;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["TModel", "Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProviderCollection"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$Expressions$ExpressionModelValueProvider$1);
var Neptuo$PresentationModels$TypeModels$Expressions$FuncFieldValueProvider$2 = {
    fullname: "Neptuo.PresentationModels.TypeModels.Expressions.FuncFieldValueProvider$2",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    interfaceNames: ["Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProvider$1"],
    Kind: "Class",
    definition: {
        ctor: function (TModel, TPropertyType, getter, setter){
            this.TModel = TModel;
            this.TPropertyType = TPropertyType;
            this._Getter = null;
            this._Setter = null;
            System.Object.ctor.call(this);
            Neptuo.Ensure.NotNull$$Object$$String(getter, "getter");
            Neptuo.Ensure.NotNull$$Object$$String(setter, "setter");
            this.set_Getter(getter);
            this.set_Setter(setter);
        },
        Getter$$: "System.Func`2[[`0],[`1]]",
        get_Getter: function (){
            return this._Getter;
        },
        set_Getter: function (value){
            this._Getter = value;
        },
        Setter$$: "System.Action`2[[`0],[`1]]",
        get_Setter: function (){
            return this._Setter;
        },
        set_Setter: function (value){
            this._Setter = value;
        },
        GetValue: function (model){
            return this.get_Getter()(model);
        },
        SetValue: function (model, value){
            this.get_Setter()(model, Cast(value, this.TPropertyType));
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["System.Func", "System.Action"]
    }
    ],
    IsAbstract: true
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$Expressions$FuncFieldValueProvider$2);
var Neptuo$PresentationModels$TypeModels$Expressions$IFieldValueProvider$1 = {
    fullname: "Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProvider$1",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    Kind: "Interface",
    ctors: [],
    IsAbstract: true
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$Expressions$IFieldValueProvider$1);
var Neptuo$PresentationModels$TypeModels$Expressions$IFieldValueProviderCollection$1 = {
    fullname: "Neptuo.PresentationModels.TypeModels.Expressions.IFieldValueProviderCollection$1",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    Kind: "Interface",
    ctors: [],
    IsAbstract: true
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$Expressions$IFieldValueProviderCollection$1);
var Neptuo$PresentationModels$TypeModels$IAttributeMetadataReader = {
    fullname: "Neptuo.PresentationModels.TypeModels.IAttributeMetadataReader",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    Kind: "Interface",
    ctors: [],
    IsAbstract: true
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$IAttributeMetadataReader);
var Neptuo$PresentationModels$TypeModels$AttributeMetadataReaderCollection = {
    fullname: "Neptuo.PresentationModels.TypeModels.AttributeMetadataReaderCollection",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    Kind: "Class",
    definition: {
        ctor: function (){
            this._Values = null;
            System.Object.ctor.call(this);
            this.set_Values(new System.Collections.Generic.Dictionary$2.ctor(System.Type.ctor, Neptuo.PresentationModels.TypeModels.IAttributeMetadataReader.ctor));
        },
        Values$$: "System.Collections.Generic.Dictionary`2[[System.Type],[Neptuo.PresentationModels.TypeModels.IAttributeMetadataReader]]",
        get_Values: function (){
            return this._Values;
        },
        set_Values: function (value){
            this._Values = value;
        },
        Add: function (attributeType, reader){
            if (System.Type.op_Equality$$Type$$Type(attributeType, null))
                throw $CreateException(new System.ArgumentNullException.ctor$$String("attributeType"), new Error());
            if (reader == null)
                throw $CreateException(new System.ArgumentNullException.ctor$$String("reader"), new Error());
            this.get_Values().set_Item$$TKey(attributeType, reader);
            return this;
        },
        TryGet: function (attributeType, reader){
            return this.get_Values().TryGetValue(attributeType, reader);
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$AttributeMetadataReaderCollection);
var Neptuo$PresentationModels$TypeModels$AttributeMetadataReaderBase$1 = {
    fullname: "Neptuo.PresentationModels.TypeModels.AttributeMetadataReaderBase$1",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    interfaceNames: ["Neptuo.PresentationModels.TypeModels.IAttributeMetadataReader"],
    Kind: "Class",
    definition: {
        ctor: function (T){
            this.T = T;
            System.Object.ctor.call(this);
        },
        Apply: function (attribute, builder){
            Neptuo.Ensure.NotNull$$Object$$String(attribute, "attribute");
            Neptuo.Ensure.NotNull$$Object$$String(builder, "builder");
            var targetAttribute = As(attribute, this.T);
            if (targetAttribute == null)
                throw $CreateException(Neptuo._EnsureSystemExtensions.InvalidOperation(Neptuo.Ensure.Exception, "Reader can process only attribute of type \'{0}\'", Typeof(this.T).get_FullName()), new Error());
            this.ApplyInternal(targetAttribute, builder);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: true
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$AttributeMetadataReaderBase$1);
var Neptuo$PresentationModels$TypeModels$ReflectionModelDefinitionBuilder = {
    fullname: "Neptuo.PresentationModels.TypeModels.ReflectionModelDefinitionBuilder",
    baseTypeName: "Neptuo.PresentationModels.ModelDefinitionBuilderBase",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    Kind: "Class",
    definition: {
        ctor: function (modelType, metadataReaderCollection){
            this._ModelType = null;
            this._MetadataReaderCollection = null;
            Neptuo.PresentationModels.ModelDefinitionBuilderBase.ctor.call(this);
            Neptuo.Ensure.NotNull$$Object$$String(modelType, "modelType");
            Neptuo.Ensure.NotNull$$Object$$String(metadataReaderCollection, "metadataReaderCollection");
            this.set_ModelType(modelType);
            this.set_MetadataReaderCollection(metadataReaderCollection);
        },
        ModelType$$: "System.Type",
        get_ModelType: function (){
            return this._ModelType;
        },
        set_ModelType: function (value){
            this._ModelType = value;
        },
        MetadataReaderCollection$$: "Neptuo.PresentationModels.TypeModels.AttributeMetadataReaderCollection",
        get_MetadataReaderCollection: function (){
            return this._MetadataReaderCollection;
        },
        set_MetadataReaderCollection: function (value){
            this._MetadataReaderCollection = value;
        },
        BuildModelIdentifier: function (){
            return this.BuildIdentifier(this.get_ModelType());
        },
        BuildFieldDefinitions: function (){
            var fields = new System.Collections.Generic.List$1.ctor(Neptuo.PresentationModels.IFieldDefinition.ctor);
            var $it1 = this.get_ModelType().GetProperties().GetEnumerator();
            while ($it1.MoveNext()){
                var propertyInfo = $it1.get_Current();
                fields.Add(new Neptuo.PresentationModels.FieldDefinition.ctor(this.BuildIdentifier(propertyInfo), propertyInfo.get_PropertyType(), this.BuildMetadata(propertyInfo)));
            }
            return fields;
        },
        BuildModelMetadata: function (){
            return this.BuildMetadata(this.get_ModelType());
        },
        BuildMetadata: function (memberInfo){
            var collection = new Neptuo.PresentationModels.MetadataCollection.ctor();
            var $it2 = memberInfo.GetCustomAttributes$$Boolean(true).GetEnumerator();
            while ($it2.MoveNext()){
                var attribute = $it2.get_Current();
                var reader = As(attribute, Neptuo.PresentationModels.TypeModels.IAttributeMetadataReader.ctor);
                if (reader != null)
                    reader.Apply(attribute, collection);
                else if ((function (){
                    var $1 = {
                        Value: reader
                    };
                    var $res = this.get_MetadataReaderCollection().TryGet(attribute.GetType(), $1);
                    reader = $1.Value;
                    return $res;
                }).call(this))
                    reader.Apply(attribute, collection);
            }
            return collection;
        },
        BuildIdentifier: function (memberInfo){
            return memberInfo.get_Name();
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["System.Type", "Neptuo.PresentationModels.TypeModels.AttributeMetadataReaderCollection"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$ReflectionModelDefinitionBuilder);
var Neptuo$PresentationModels$TypeModels$ReflectionModelValueProvider$1 = {
    fullname: "Neptuo.PresentationModels.TypeModels.ReflectionModelValueProvider$1",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    interfaceNames: ["Neptuo.PresentationModels.IModelValueProvider"],
    Kind: "Class",
    definition: {
        ctor: function (TModel, model){
            this.TModel = TModel;
            this.properties = new System.Collections.Generic.Dictionary$2.ctor(System.String.ctor, System.Reflection.PropertyInfo.ctor);
            this._ModelType = null;
            this._Model = null;
            System.Object.ctor.call(this);
            Neptuo.Ensure.NotNull$$Object$$String(model, "model");
            this.set_Model(model);
            this.set_ModelType(model.GetType());
        },
        ModelType$$: "System.Type",
        get_ModelType: function (){
            return this._ModelType;
        },
        set_ModelType: function (value){
            this._ModelType = value;
        },
        Model$$: "`0",
        get_Model: function (){
            return this._Model;
        },
        set_Model: function (value){
            this._Model = value;
        },
        TryGetValue: function (identifier, value){
            Neptuo.Ensure.NotNullOrEmpty(identifier, "identifier");
            var propertyInfo;
            if ((function (){
                var $1 = {
                    Value: propertyInfo
                };
                var $res = this.TryGetPropertyInfo(identifier, $1);
                propertyInfo = $1.Value;
                return $res;
            }).call(this)){
                value.Value = propertyInfo.GetValue$$Object(this.get_Model());
                return true;
            }
            value.Value = null;
            return false;
        },
        TrySetValue: function (identifier, value){
            Neptuo.Ensure.NotNullOrEmpty(identifier, "identifier");
            var propertyInfo;
            if ((function (){
                var $1 = {
                    Value: propertyInfo
                };
                var $res = this.TryGetPropertyInfo(identifier, $1);
                propertyInfo = $1.Value;
                return $res;
            }).call(this)){
                if (value != null && !propertyInfo.get_PropertyType().IsAssignableFrom(value.GetType())){
                    var typeConverter = System.ComponentModel.TypeDescriptor.GetConverter$$Type(propertyInfo.get_PropertyType());
                    if (typeConverter != null)
                        value = typeConverter.ConvertFrom$$Object(value);
                }
                propertyInfo.SetValue$$Object$$Object(this.get_Model(), value);
                return true;
            }
            return false;
        },
        TryGetPropertyInfo: function (identifier, propertyInfo){
            if (this.properties.TryGetValue(identifier, propertyInfo))
                return true;
            propertyInfo.Value = this.get_ModelType().GetProperty$$String(identifier);
            if (System.Reflection.PropertyInfo.op_Inequality$$PropertyInfo$$PropertyInfo(propertyInfo.Value, null))
                this.properties.set_Item$$TKey(identifier, propertyInfo.Value);
            return System.Reflection.PropertyInfo.op_Inequality$$PropertyInfo$$PropertyInfo(propertyInfo.Value, null);
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["TModel"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$ReflectionModelValueProvider$1);
var Neptuo$PresentationModels$TypeModels$ReflectionModelValueProvider = {
    fullname: "Neptuo.PresentationModels.TypeModels.ReflectionModelValueProvider",
    baseTypeName: "Neptuo.PresentationModels.TypeModels.ReflectionModelValueProvider$1",
    assemblyName: "Neptuo.PresentationModels.TypeModels",
    Kind: "Class",
    definition: {
        ctor: function (model){
            Neptuo.PresentationModels.TypeModels.ReflectionModelValueProvider$1.ctor.call(this, System.Object.ctor, model);
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["System.Object"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$PresentationModels$TypeModels$ReflectionModelValueProvider);

