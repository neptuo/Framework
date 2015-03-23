This repository contains base framework libraries which are used as mscorlib extensions.

* [Activators](./wiki/Activators)
* [[AppServices|AppServices]]
* [[Behaviors|Behaviors]]
* [[Bootstrap|Bootstrap]] (Neptuo.Bootstrap)
* [[Compilers|Compilers]]
* [[Converters|Converters]]
* [[DomainModels|DomainModels]]
* [[FeatureModels|FeatureModels]]
* [[FileSystems|FileSystems]]
* [[Migrations|Migrations]] (Neptuo.Migrations)
* [[Pipelines|Pipelines]]
* [[PresentationModels|PresentationModels]] (Neptuo.PresentationModels)
* [[StateMachines|StateMachines]]
* [[Tokens|Tokens]]

**Neptuo.dll**

Contracts and base implementations are typicaly placed in the Neptuo(.dll), so referencing this library makes most of them available. The design goal after that is quite simple

> Neptuo.dll references only System a System.Core, so contracts and base implementations of everything that is reusable during application lifetime and doesn't required other references, can be placed here.

For e.g. Bootstrap is required only when starting application, so this is separated into its own library/dll.
