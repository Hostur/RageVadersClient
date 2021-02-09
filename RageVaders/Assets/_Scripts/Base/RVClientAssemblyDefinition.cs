using Autofac;
using Client;
using RageVadersData;
using RageVadersData.Client;
using RageVadersModules;

public class RVClientAssemblyDefinition : RVAssemblyDefinition
{
	private readonly IRVModulesConfiguration _modulesConfiguration;

	public RVClientAssemblyDefinition(IRVModulesConfiguration modulesConfiguration, IRVNetworkSettings networkSettings) : base(networkSettings)
	{
		_modulesConfiguration = modulesConfiguration;
	}

	public override void Register(ContainerBuilder builder, IRVLogger logger)
	{
		builder.Register(c => logger)
			.As<IRVLogger>()
			.SingleInstance();

		builder.Register(c => new ClientDisconnectionHandler())
			.As<ClientDisconnectionHandler>()
			.As<IClientDisconnectionHandler>()
			.SingleInstance();

		builder.Register(c => _modulesConfiguration)
			.As<IRVModulesConfiguration>()
			.SingleInstance();

		builder.Register(c => NetworkSettings)
			.As<IRVNetworkSettings>()
			.Keyed<object>(typeof(IRVNetworkSettings).FullName)
			.SingleInstance();

		RegisterViewModels(builder);
	}

	private void RegisterViewModels(ContainerBuilder builder)
	{
		
	}
}
