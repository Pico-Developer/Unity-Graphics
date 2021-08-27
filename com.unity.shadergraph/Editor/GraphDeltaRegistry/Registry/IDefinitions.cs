using UnityEngine;

using UnityEditor.ShaderGraph.GraphDelta;

namespace UnityEditor.ShaderGraph.Registry.Defs
{
    public interface IRegistryEntry
    {
        RegistryKey GetRegistryKey();

        // Some flags should be automatically populated based on the definition interface used.
        // This whole concept may prove unnecessary.
        RegistryFlags GetRegistryFlags();
    }

    public interface INodeDefinitionBuilder : IRegistryEntry
    {
        void BuildNode(INodeReader userData, INodeWriter generatedData, Registry registry);
        ShaderFoundry.ShaderFunction GetShaderFunction(INodeReader data, ShaderFoundry.ShaderContainer container, Registry registry);
    }

    public interface ITypeDefinitionBuilder : IRegistryEntry
    {
        void BuildType(IFieldReader userData, IFieldWriter generatedData, Registry registry);
        ShaderFoundry.ShaderType GetShaderType(IFieldReader data, ShaderFoundry.ShaderContainer container, Registry registry);
    }

    public interface ICastDefinitionBuilder : IRegistryEntry
    {
        // Gross- but should suffice for now.
        (RegistryKey, RegistryKey) GetTypeConversionMapping();

        // TypeConversionMapping represents the Types we can convert, but TypeDefinitions can represent templated concepts,
        // which may mean that incompatibilities within their data could be inconvertible. Types with static fields should
        // implement an ITypeConversion with itself to ensure that static concepts can be represented.
        bool CanConvert(IFieldReader src, IFieldReader dst);

        ShaderFoundry.ShaderFunction GetShaderCast(IFieldReader src, IFieldReader dst, ShaderFoundry.ShaderContainer container, Registry registry);
    }
}
