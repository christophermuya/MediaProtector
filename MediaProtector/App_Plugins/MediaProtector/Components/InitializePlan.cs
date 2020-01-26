using MediaProtector.App_Plugins.MediaProtector.Events;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;

namespace MediaProtector.App_Plugins.MediaProtector.Components {
    public class InitializePlan:IComponent {

        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;
        public InitializePlan(IScopeProvider scopeProvider,IMigrationBuilder migrationBuilder,IKeyValueService keyValueService,ILogger logger) {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
        }
        public void Initialize() {
            // Create a migration plan for a specific project/feature
            // We can then track that latest migration state/step for this project/feature.
            /*
                A plan is executed by an upgrader. The upgrader compares the final state of the plan against the current state, and executes all migrations until it reaches the final state. 
             */
            var migrationPlan = new MigrationPlan("MediaProtector");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            //More info at https://www.zpqrtbnk.net/posts/migrations-in-v8
            migrationPlan.From(string.Empty)
                .To<CreateTableAndData>("Initialized-MediaProtector")
                .To<AddDisableEventColumn>("Add-disableEvent-Column");

            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider,_migrationBuilder,_keyValueService,_logger);
        }
        public void Terminate() {
        }
    }
}