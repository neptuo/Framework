/* Generated by SharpKit 5 v5.4.4 */
function $CombineDelegates(del1,del2)
{
    if(del1 == null)
        return del2;
    if(del2 == null)
        return del1;
    var del=$CreateMulticastDelegateFunction();
    del.delegates = [];
    if(del1.isMulticastDelegate)
    {
        for(var i=0;i < del1.delegates.length;i++)
            del.delegates.push(del1.delegates[i]);
    }
    else
    {
        del.delegates.push(del1);
    }
    if(del2.isMulticastDelegate)
    {
        for(var i=0;i < del2.delegates.length;i++)
            del.delegates.push(del2.delegates[i]);
    }
    else
    {
        del.delegates.push(del2);
    }
    return del;
};

function $CreateMulticastDelegateFunction()
{
    var del2 = null;
    
    var del=function()
    {
        var x=undefined;
        for(var i=0;i < del2.delegates.length;i++)
        {
            var del3=del2.delegates[i];
            x = del3.apply(null,arguments);
        }
        return x;
    };
    del.isMulticastDelegate = true;
    del2 = del;   
    
    return del;
};

function $RemoveDelegate(delOriginal,delToRemove)
{
    if(delToRemove == null || delOriginal == null)
        return delOriginal;
    if(delOriginal.isMulticastDelegate)
    {
        if(delToRemove.isMulticastDelegate)
            throw new Error("Multicast to multicast delegate removal is not implemented yet");
        var del=$CreateMulticastDelegateFunction();
        for(var i=0;i < delOriginal.delegates.length;i++)
        {
            var del2=delOriginal.delegates[i];
            if(del2 != delToRemove)
            {
                if(del.delegates == null)
                    del.delegates = [];
                del.delegates.push(del2);
            }
        }
        if(del.delegates == null)
            return null;
        if(del.delegates.length == 1)
            return del.delegates[0];
        return del;
    }
    else
    {
        if(delToRemove.isMulticastDelegate)
            throw new Error("single to multicast delegate removal is not supported");
        if(delOriginal == delToRemove)
            return null;
        return delOriginal;
    }
};

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

if (typeof ($CreateAnonymousDelegate) == 'undefined') {
    var $CreateAnonymousDelegate = function (target, func) {
        if (target == null || func == null)
            return func;
        var delegate = function () {
            return func.apply(target, arguments);
        };
        delegate.func = func;
        delegate.target = target;
        delegate.isDelegate = true;
        return delegate;
    }
}


if (typeof(JsTypes) == "undefined")
    var JsTypes = [];
var Neptuo$Tokens$VersionInfo = {
    fullname: "Neptuo.Tokens.VersionInfo",
    baseTypeName: "System.Object",
    staticDefinition: {
        cctor: function (){
            Neptuo.Tokens.VersionInfo.Version = "2.0.0";
        },
        GetVersion: function (){
            return new System.Version.ctor$$String("2.0.0");
        }
    },
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            System.Object.ctor.call(this);
        }
    },
    ctors: [],
    IsAbstract: true
};
JsTypes.push(Neptuo$Tokens$VersionInfo);
var Neptuo$Tokens$Token = {
    fullname: "Neptuo.Tokens.Token",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this._Prefix = null;
            this._Name = null;
            this._Attributes = null;
            this._DefaultAttributeValue = null;
            System.Object.ctor.call(this);
            this.set_Attributes(new System.Collections.Generic.List$1.ctor(Neptuo.Tokens.TokenAttribute.ctor));
        },
        Prefix$$: "System.String",
        get_Prefix: function (){
            return this._Prefix;
        },
        set_Prefix: function (value){
            this._Prefix = value;
        },
        Name$$: "System.String",
        get_Name: function (){
            return this._Name;
        },
        set_Name: function (value){
            this._Name = value;
        },
        Fullname$$: "System.String",
        get_Fullname: function (){
            if (System.String.IsNullOrEmpty(this.get_Prefix()))
                return this.get_Name();
            return this.get_Prefix() + ":" + this.get_Name();
        },
        set_Fullname: function (value){
            var index = value.IndexOf$$Char(":");
            if (index != -1){
                this.set_Prefix(value.Substring$$Int32$$Int32(0, index));
                this.set_Name(value.Substring$$Int32(index + 1));
            }
            else {
                this.set_Name(value);
            }
        },
        Attributes$$: "System.Collections.Generic.List`1[[Neptuo.Tokens.TokenAttribute]]",
        get_Attributes: function (){
            return this._Attributes;
        },
        set_Attributes: function (value){
            this._Attributes = value;
        },
        DefaultAttributeValue$$: "System.String",
        get_DefaultAttributeValue: function (){
            return this._DefaultAttributeValue;
        },
        set_DefaultAttributeValue: function (value){
            this._DefaultAttributeValue = value;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$Token);
var Neptuo$Tokens$TokenAttribute = {
    fullname: "Neptuo.Tokens.TokenAttribute",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (name, value){
            this._Name = null;
            this._Value = null;
            System.Object.ctor.call(this);
            this.set_Name(name);
            this.set_Value(value);
        },
        Name$$: "System.String",
        get_Name: function (){
            return this._Name;
        },
        set_Name: function (value){
            this._Name = value;
        },
        Value$$: "System.String",
        get_Value: function (){
            return this._Value;
        },
        set_Value: function (value){
            this._Value = value;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["System.String", "System.String"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenAttribute);
var Neptuo$Tokens$TokenEventArgs = {
    fullname: "Neptuo.Tokens.TokenEventArgs",
    baseTypeName: "System.EventArgs",
    staticDefinition: {
        cctor: function (){
        }
    },
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (originalContent, token, startPosition, endPosition){
            this._OriginalContent = null;
            this._Token = null;
            this._StartPosition = 0;
            this._EndPosition = 0;
            System.EventArgs.ctor.call(this);
            this.set_OriginalContent(originalContent);
            this.set_Token(token);
            this.set_StartPosition(startPosition);
            this.set_EndPosition(endPosition);
        },
        OriginalContent$$: "System.String",
        get_OriginalContent: function (){
            return this._OriginalContent;
        },
        set_OriginalContent: function (value){
            this._OriginalContent = value;
        },
        Token$$: "Neptuo.Tokens.Token",
        get_Token: function (){
            return this._Token;
        },
        set_Token: function (value){
            this._Token = value;
        },
        StartPosition$$: "System.Int32",
        get_StartPosition: function (){
            return this._StartPosition;
        },
        set_StartPosition: function (value){
            this._StartPosition = value;
        },
        EndPosition$$: "System.Int32",
        get_EndPosition: function (){
            return this._EndPosition;
        },
        set_EndPosition: function (value){
            this._EndPosition = value;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["System.String", "Neptuo.Tokens.Token", "System.Int32", "System.Int32"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenEventArgs);
var Neptuo$Tokens$TokenParser = {
    fullname: "Neptuo.Tokens.TokenParser",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this.OnParsedToken = null;
            this._Configuration = null;
            System.Object.ctor.call(this);
            this.set_Configuration(new Neptuo.Tokens.TokenParserConfiguration.ctor());
        },
        Configuration$$: "Neptuo.Tokens.TokenParserConfiguration",
        get_Configuration: function (){
            return this._Configuration;
        },
        set_Configuration: function (value){
            this._Configuration = value;
        },
        add_OnParsedToken: function (value){
            this.OnParsedToken = $CombineDelegates(this.OnParsedToken, value);
        },
        remove_OnParsedToken: function (value){
            this.OnParsedToken = $RemoveDelegate(this.OnParsedToken, value);
        },
        Parse: function (content){
            if (System.MulticastDelegate.op_Equality$$MulticastDelegate$$MulticastDelegate(this.OnParsedToken, null))
                throw $CreateException(new System.InvalidOperationException.ctor$$String("OnParsedItem is null."), new Error());
            var results = new System.Collections.Generic.List$1.ctor(Neptuo.Tokens.TokenStateMachine.Result.ctor);
            var stateMachine = new Neptuo.Tokens.TokenStateMachine.ctor(this.GetStateMachineConfiguration());
            stateMachine.OnEnterConcreteState$1(Neptuo.Tokens.TokenDoneState.ctor, $CreateAnonymousDelegate(this, function (sender, e){
                results.Add(e.get_State().GetResult());
            }));
            if (System.Type.op_Inequality$$Type$$Type(stateMachine.Process(content).GetType(), Typeof(Neptuo.Tokens.TokenErrorState.ctor))){
                var $it1 = results.GetEnumerator();
                while ($it1.MoveNext()){
                    var result = $it1.get_Current();
                    this.OnParsedToken(this, new Neptuo.Tokens.TokenEventArgs.ctor(content, result.get_Token(), result.get_StartIndex(), result.get_LastIndex()));
                }
                return true;
            }
            return false;
        },
        GetStateMachineConfiguration: function (){
            return (function (){
                var $v1 = new Neptuo.Tokens.TokenStateMachine.Configuration.ctor();
                $v1.set_AllowAttributes(this.get_Configuration().get_AllowAttributes());
                $v1.set_AllowEscapeSequence(this.get_Configuration().get_AllowEscapeSequence());
                $v1.set_AllowDefaultAttribute(this.get_Configuration().get_AllowDefaultAttribute());
                $v1.set_AllowMultipleTokens(this.get_Configuration().get_AllowMultipleTokens());
                $v1.set_AllowTextContent(this.get_Configuration().get_AllowTextContent());
                return $v1;
            }).call(this);
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenParser);
var Neptuo$Tokens$TokenParserConfiguration = {
    fullname: "Neptuo.Tokens.TokenParserConfiguration",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this._AllowAttributes = false;
            this._AllowEscapeSequence = false;
            this._AllowDefaultAttribute = false;
            this._AllowMultipleTokens = false;
            this._AllowTextContent = false;
            System.Object.ctor.call(this);
        },
        AllowAttributes$$: "System.Boolean",
        get_AllowAttributes: function (){
            return this._AllowAttributes;
        },
        set_AllowAttributes: function (value){
            this._AllowAttributes = value;
        },
        AllowEscapeSequence$$: "System.Boolean",
        get_AllowEscapeSequence: function (){
            return this._AllowEscapeSequence;
        },
        set_AllowEscapeSequence: function (value){
            this._AllowEscapeSequence = value;
        },
        AllowDefaultAttribute$$: "System.Boolean",
        get_AllowDefaultAttribute: function (){
            return this._AllowDefaultAttribute;
        },
        set_AllowDefaultAttribute: function (value){
            this._AllowDefaultAttribute = value;
        },
        AllowMultipleTokens$$: "System.Boolean",
        get_AllowMultipleTokens: function (){
            return this._AllowMultipleTokens;
        },
        set_AllowMultipleTokens: function (value){
            this._AllowMultipleTokens = value;
        },
        AllowTextContent$$: "System.Boolean",
        get_AllowTextContent: function (){
            return this._AllowTextContent;
        },
        set_AllowTextContent: function (value){
            this._AllowTextContent = value;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenParserConfiguration);
var Neptuo$Tokens$TokenStateMachine = {
    fullname: "Neptuo.Tokens.TokenStateMachine",
    baseTypeName: "Neptuo.StateMachines.StringStateMachine$1",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (configuration){
            Neptuo.StateMachines.StringStateMachine$1.ctor.call(this, Neptuo.Tokens.TokenState.ctor, new Neptuo.Tokens.TokenStartState.ctor$$Configuration(configuration));
        }
    },
    ctors: [{
        name: "ctor",
        parameters: ["Neptuo.Tokens.TokenStateMachine.Configuration"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenStateMachine);
var Neptuo$Tokens$TokenStateMachine$Configuration = {
    fullname: "Neptuo.Tokens.TokenStateMachine.Configuration",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this._AllowTextContent = false;
            this._AllowMultipleTokens = false;
            this._AllowAttributes = false;
            this._AllowDefaultAttribute = false;
            this._AllowEscapeSequence = false;
            System.Object.ctor.call(this);
        },
        AllowTextContent$$: "System.Boolean",
        get_AllowTextContent: function (){
            return this._AllowTextContent;
        },
        set_AllowTextContent: function (value){
            this._AllowTextContent = value;
        },
        AllowMultipleTokens$$: "System.Boolean",
        get_AllowMultipleTokens: function (){
            return this._AllowMultipleTokens;
        },
        set_AllowMultipleTokens: function (value){
            this._AllowMultipleTokens = value;
        },
        AllowAttributes$$: "System.Boolean",
        get_AllowAttributes: function (){
            return this._AllowAttributes;
        },
        set_AllowAttributes: function (value){
            this._AllowAttributes = value;
        },
        AllowDefaultAttribute$$: "System.Boolean",
        get_AllowDefaultAttribute: function (){
            return this._AllowDefaultAttribute;
        },
        set_AllowDefaultAttribute: function (value){
            this._AllowDefaultAttribute = value;
        },
        AllowEscapeSequence$$: "System.Boolean",
        get_AllowEscapeSequence: function (){
            return this._AllowEscapeSequence;
        },
        set_AllowEscapeSequence: function (value){
            this._AllowEscapeSequence = value;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenStateMachine$Configuration);
var Neptuo$Tokens$TokenStateMachine$Result = {
    fullname: "Neptuo.Tokens.TokenStateMachine.Result",
    baseTypeName: "System.Object",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this._Token = null;
            this._StartIndex = 0;
            this._LastIndex = 0;
            System.Object.ctor.call(this);
            this.set_Token(new Neptuo.Tokens.Token.ctor());
        },
        Token$$: "Neptuo.Tokens.Token",
        get_Token: function (){
            return this._Token;
        },
        set_Token: function (value){
            this._Token = value;
        },
        StartIndex$$: "System.Int32",
        get_StartIndex: function (){
            return this._StartIndex;
        },
        set_StartIndex: function (value){
            this._StartIndex = value;
        },
        LastIndex$$: "System.Int32",
        get_LastIndex: function (){
            return this._LastIndex;
        },
        set_LastIndex: function (value){
            this._LastIndex = value;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenStateMachine$Result);
var Neptuo$Tokens$TokenState = {
    fullname: "Neptuo.Tokens.TokenState",
    baseTypeName: "Neptuo.StateMachines.StringState$2",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this._Configuration = null;
            Neptuo.StateMachines.StringState$2.ctor.call(this, Neptuo.Tokens.TokenStateMachine.Result.ctor, Neptuo.Tokens.TokenState.ctor);
        },
        Configuration$$: "Neptuo.Tokens.TokenStateMachine+Configuration",
        get_Configuration: function (){
            return this._Configuration;
        },
        set_Configuration: function (value){
            this._Configuration = value;
        },
        Move$1: function (TNewState){
            var newState = Neptuo.StateMachines.StringState$2.commonPrototype.Move$1.call(TNewState, this);
            newState.set_Configuration(this.get_Configuration());
            return newState;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: true
};
JsTypes.push(Neptuo$Tokens$TokenState);
var Neptuo$Tokens$TokenStartState = {
    fullname: "Neptuo.Tokens.TokenStartState",
    baseTypeName: "Neptuo.Tokens.TokenState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            Neptuo.Tokens.TokenState.ctor.call(this);
        },
        ctor$$Configuration: function (configuration){
            Neptuo.Tokens.TokenState.ctor.call(this);
            this.set_Context(new Neptuo.Tokens.TokenStateMachine.Result.ctor());
            this.set_Configuration(configuration);
        },
        Accept: function (input, position){
            if (input == "{")
                return this.Move$1(Neptuo.Tokens.TokenFullnameState.ctor);
            if (!this.get_Configuration().get_AllowTextContent())
                return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
            this.get_Text().Append$$Char(input);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }, {
        name: "ctor$$Configuration",
        parameters: ["Neptuo.Tokens.TokenStateMachine.Configuration"]
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenStartState);
var Neptuo$Tokens$TokenFullnameState = {
    fullname: "Neptuo.Tokens.TokenFullnameState",
    baseTypeName: "Neptuo.Tokens.TokenState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            Neptuo.Tokens.TokenState.ctor.call(this);
        },
        Accept: function (input, position){
            if (input == "{"){
                if (this.get_Text().get_Length() == 0){
                    if (this.get_Configuration().get_AllowEscapeSequence())
                        return this.Move$1(Neptuo.Tokens.TokenStartState.ctor);
                    else
                        return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
                }
                return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
            }
            if (input == " "){
                if (!this.get_Configuration().get_AllowAttributes())
                    return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
                this.get_Context().get_Token().set_Fullname(this.get_Text().ToString());
                return this.Move$1(Neptuo.Tokens.TokenFirstAttributeState.ctor);
            }
            if (input == "}"){
                this.get_Context().set_LastIndex(position);
                this.get_Context().get_Token().set_Fullname(this.get_Text().ToString());
                return this.Move$1(Neptuo.Tokens.TokenDoneState.ctor);
            }
            if (this.get_Text().get_Length() == 0)
                this.get_Context().set_StartIndex(position - 1);
            this.get_Text().Append$$Char(input);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenFullnameState);
var Neptuo$Tokens$TokenFirstAttributeState = {
    fullname: "Neptuo.Tokens.TokenFirstAttributeState",
    baseTypeName: "Neptuo.Tokens.TokenState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this.innerExtensions = 0;
            Neptuo.Tokens.TokenState.ctor.call(this);
        },
        Accept: function (input, position){
            if (input == ","){
                if (!this.get_Configuration().get_AllowDefaultAttribute())
                    return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
                this.get_Context().get_Token().set_DefaultAttributeValue(this.get_Text().ToString());
                return this.Move$1(Neptuo.Tokens.TokenAttributeNameState.ctor);
            }
            if (input == "="){
                this.get_Context().get_Token().get_Attributes().Add(new Neptuo.Tokens.TokenAttribute.ctor(this.get_Text().ToString(), null));
                return this.Move$1(Neptuo.Tokens.TokenAttributeValueState.ctor);
            }
            if (input == "}"){
                if (!this.get_Configuration().get_AllowDefaultAttribute())
                    return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
                if (this.innerExtensions == 0){
                    this.get_Context().set_LastIndex(position);
                    this.get_Context().get_Token().set_DefaultAttributeValue(this.get_Text().ToString());
                    return this.Move$1(Neptuo.Tokens.TokenDoneState.ctor);
                }
                else {
                    this.innerExtensions--;
                }
            }
            if (input == "{")
                this.innerExtensions++;
            this.get_Text().Append$$Char(input);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenFirstAttributeState);
var Neptuo$Tokens$TokenAttributeNameState = {
    fullname: "Neptuo.Tokens.TokenAttributeNameState",
    baseTypeName: "Neptuo.Tokens.TokenState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            Neptuo.Tokens.TokenState.ctor.call(this);
        },
        Accept: function (input, position){
            if (input == "="){
                this.get_Context().get_Token().get_Attributes().Add(new Neptuo.Tokens.TokenAttribute.ctor(this.get_Text().ToString(), null));
                return this.Move$1(Neptuo.Tokens.TokenAttributeValueState.ctor);
            }
            if (input == " " && this.get_Text().get_Length() == 0)
                return this;
            if (input == "{" || input == "}" || input == "," || input == " ")
                return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
            this.get_Text().Append$$Char(input);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenAttributeNameState);
var Neptuo$Tokens$TokenAttributeValueState = {
    fullname: "Neptuo.Tokens.TokenAttributeValueState",
    baseTypeName: "Neptuo.Tokens.TokenState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            this.innerExtensions = 0;
            Neptuo.Tokens.TokenState.ctor.call(this);
        },
        Accept: function (input, position){
            if (input == ","){
                System.Linq.Enumerable.LastOrDefault$1$$IEnumerable$1(Neptuo.Tokens.TokenAttribute.ctor, this.get_Context().get_Token().get_Attributes()).set_Value(this.get_Text().ToString());
                return this.Move$1(Neptuo.Tokens.TokenAttributeNameState.ctor);
            }
            if (input == "}"){
                if (this.innerExtensions == 0){
                    this.get_Context().set_LastIndex(position);
                    System.Linq.Enumerable.LastOrDefault$1$$IEnumerable$1(Neptuo.Tokens.TokenAttribute.ctor, this.get_Context().get_Token().get_Attributes()).set_Value(this.get_Text().ToString());
                    return this.Move$1(Neptuo.Tokens.TokenDoneState.ctor);
                }
                else {
                    this.innerExtensions--;
                }
            }
            if (input == "{")
                this.innerExtensions++;
            this.get_Text().Append$$Char(input);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenAttributeValueState);
var Neptuo$Tokens$TokenDoneState = {
    fullname: "Neptuo.Tokens.TokenDoneState",
    baseTypeName: "Neptuo.Tokens.TokenStartState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            Neptuo.Tokens.TokenStartState.ctor.call(this);
        },
        GetContextForNewState: function (){
            return new Neptuo.Tokens.TokenStateMachine.Result.ctor();
        },
        Accept: function (input, position){
            if (!this.get_Configuration().get_AllowMultipleTokens() && input == "{")
                return this.Move$1(Neptuo.Tokens.TokenErrorState.ctor);
            var newState = Neptuo.Tokens.TokenStartState.commonPrototype.Accept.call(this, input, position);
            return newState;
        },
        GetResult: function (){
            return this.get_Context();
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenDoneState);
var Neptuo$Tokens$TokenErrorState = {
    fullname: "Neptuo.Tokens.TokenErrorState",
    baseTypeName: "Neptuo.Tokens.TokenState",
    assemblyName: "Neptuo.Tokens",
    Kind: "Class",
    definition: {
        ctor: function (){
            Neptuo.Tokens.TokenState.ctor.call(this);
        },
        Accept: function (input, position){
            this.get_Text().Append$$Char(input);
            return this;
        }
    },
    ctors: [{
        name: "ctor",
        parameters: []
    }
    ],
    IsAbstract: false
};
JsTypes.push(Neptuo$Tokens$TokenErrorState);
