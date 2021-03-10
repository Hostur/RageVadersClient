using Autofac;
using Client;
using Gameplay.Ship;
using Gameplay.Views;
using Localization;
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

		builder.Register(c => new SpawnedGameEntities(c.Resolve<IRVNetworkSettings>()))
			.As<SpawnedGameEntities>()
			.Keyed<object>(typeof(SpawnedGameEntities).FullName)
			.SingleInstance();

		builder.Register(c => new GameTranslator())
			.As<GameTranslator>()
			.Keyed<object>(typeof(GameTranslator).FullName)
			.SingleInstance();

		RegisterViewModels(builder);
	}

	private void RegisterViewModels(ContainerBuilder builder)
	{
		builder.Register(c => new MenuViewModel(c.Resolve<RVMainThreadActionsQueue>()))
			.As<MenuViewModel>()
			.Keyed<object>(typeof(MenuViewModel).FullName)
			.InstancePerDependency();

		builder.Register(c => new PlayerViewModel(
				c.Resolve<RVClientPlayersData>(),
				c.Resolve<RVClientNetworkData>()))
			.As<PlayerViewModel>()
			.Keyed<object>(typeof(PlayerViewModel).FullName)
			.InstancePerDependency();
	}
}
