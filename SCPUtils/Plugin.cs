namespace SCPUtils
{
    using PluginAPI.Core;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Events;
    using SCPUtils.Commands;

    public class ScpUtils
    {
        public EventHandlers EventHandlers { get; private set; }
        public Functions Functions { get; private set; }
        public Player Player { get; private set; }
        public Database DatabasePlayerData { get; private set; }
        public Events.Events Events { get; private set; }
        public int PatchesCounter { get; private set; }

        /*static readonly ScpUtils InstanceValue = new ScpUtils();
        ScpUtils()
        {

        }

        internal static ScpUtils StaticInstance => InstanceValue;*/

        /*public static readonly ScpUtils InstanceValue = new ScpUtils();
        public ScpUtils()
        {

        }*/

        public static ScpUtils StaticInstance;

        [PluginPriority(PluginAPI.Enums.LoadPriority.Medium)]
        [PluginEntryPoint("SCPUtils", "6.0.0", "The most famous plugin that offers many additions to the servers.", "Terminator_97")]
        public void LoadPlugin()
        {
            if (configs.IsEnabled is false) return;

            StaticInstance = this;

            Functions = new Functions(this);
            EventHandlers = new EventHandlers();
            DatabasePlayerData = new Database(this);
            Events = new Events.Events(this);

            EventManager.RegisterEvents<EventHandlers>(this);

            var handler = PluginHandler.Get(this);

            handler.SaveConfig(this, nameof(Configs));
            handler.SaveConfig(this, nameof(Permissions));
            handler.SaveConfig(this, nameof(CommandTranslation));

            DatabasePlayerData.CreateDatabase();
            DatabasePlayerData.OpenDatabase();
        }

        /*public void LoadEvents()
        {
            MapEvents.Decontaminating += EventHandlers.OnDecontaminate;
            PlayerEvents.Verified += EventHandlers.OnPlayerVerify;
            PlayerEvents.Destroying += EventHandlers.OnPlayerDestroy;
            PlayerEvents.Spawning += EventHandlers.OnPlayerSpawn;
            PlayerEvents.Dying += EventHandlers.OnPlayerDeath;
            PlayerEvents.Hurting += EventHandlers.OnPlayerHurt;
            PlayerEvents.PreAuthenticating += EventHandlers.OnPlayerPreauth;
            Handlers.Scp079.InteractingTesla += EventHandlers.On079TeslaEvent;
            ServerEvents.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            ServerEvents.RoundEnded += EventHandlers.OnRoundEnded;
            Handlers.Scp096.AddingTarget += EventHandlers.On096AddTarget;
            ServerEvents.RespawningTeam += EventHandlers.OnTeamRespawn;
            ServerEvents.RestartingRound += EventHandlers.OnRoundRestart;
            PlayerEvents.Handcuffing += EventHandlers.OnPlayerHandcuff;
            PlayerEvents.RemovingHandcuffs += EventHandlers.OnPlayerUnhandCuff;
            PlayerEvents.Banning += EventHandlers.OnBanned;
            PlayerEvents.Kicking += EventHandlers.OnKicking;
            PlayerEvents.ChangingRole += EventHandlers.OnChangingRole;
            ServerEvents.RoundStarted += EventHandlers.OnRoundStarted;
            PlayerEvents.TogglingOverwatch += EventHandlers.OnOverwatchToggle;
        }

        public void OnEnabled()
        {
            Functions = new Functions(this);
            EventHandlers = new EventHandlers(this);
            DatabasePlayerData = new Database(this);
            Events = new Events.Events(this);

            EventHandlers.TemporarilyDisabledWarns = false;

            if (Config.EnableAutoRestart)
            {
                Functions.CoroutineRestart();
            }

            LoadEvents();
            DatabasePlayerData.CreateDatabase();
            DatabasePlayerData.OpenDatabase();
        }

        public void OnDisabled()
        {
            MapEvents.Decontaminating -= EventHandlers.OnDecontaminate;
            PlayerEvents.Verified -= EventHandlers.OnPlayerVerify;
            PlayerEvents.Destroying -= EventHandlers.OnPlayerDestroy;
            PlayerEvents.Spawning -= EventHandlers.OnPlayerSpawn;
            PlayerEvents.Dying -= EventHandlers.OnPlayerDeath;
            PlayerEvents.Hurting -= EventHandlers.OnPlayerHurt;
            PlayerEvents.PreAuthenticating -= EventHandlers.OnPlayerPreauth;
            Handlers.Scp079.InteractingTesla -= EventHandlers.On079TeslaEvent;
            ServerEvents.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            ServerEvents.RoundEnded -= EventHandlers.OnRoundEnded;
            Handlers.Scp096.AddingTarget -= EventHandlers.On096AddTarget;
            ServerEvents.RespawningTeam -= EventHandlers.OnTeamRespawn;
            ServerEvents.RestartingRound -= EventHandlers.OnRoundRestart;
            PlayerEvents.Handcuffing -= EventHandlers.OnPlayerHandcuff;
            PlayerEvents.RemovingHandcuffs -= EventHandlers.OnPlayerUnhandCuff;
            PlayerEvents.Banning -= EventHandlers.OnBanned;
            PlayerEvents.Kicking -= EventHandlers.OnKicking;
            //PlayerEvents.TogglingOverwatch -= EventHandlers.OnOverwatchToggle;
            //PlayerEvents.ChangingRole -= EventHandlers.OnChangingRole;
            //ServerEvents.RoundStarted -= EventHandlers.OnRoundStarted;
            EventHandlers = null;
            Functions = null;
            Functions.LastWarn.Clear();
            Database.LiteDatabase.Dispose();
        }*/

        [PluginConfig] public Configs configs;

        [PluginConfig("permissions.yml")] public Permissions perms;

        [PluginConfig("commands.yml")] public CommandTranslation commandTranslation;
    }
}
