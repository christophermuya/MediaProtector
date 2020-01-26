using MediaProtector.App_Plugins.MediaProtector.Models;
using System;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Scoping;

namespace MediaProtector.App_Plugins.MediaProtector.Events {
    public class CreateTableAndData:MigrationBase {

        private readonly IScopeProvider _scopeProvider;
        public CreateTableAndData(IMigrationContext context,IScopeProvider scopeProvider) : base(context) {
            _scopeProvider = scopeProvider;
        }
        public override void Migrate() {
            // Lots of methods available in the MigrationBase class - discover with this.
            if(TableExists("MediaProtector") == false) {
                Create.Table<EventModel>().Do();
            }
            else {
                Logger.Info<CreateTableAndData>("The database table Media Protector already exists, skipping","Media Protector");
            }
            if(TableExists("MediaProtector")) {
                try {
                    EventModel saveData = new EventModel() {
                        id = 1,
                        eventType = "save",
                        lastEdited = null,
                        lastEditedBy = "",
                        mediaNodes = "",
                        userExceptions = "",
                        expansion = null,
                        disableEvent = false
                    };

                    EventModel moveData = new EventModel() {
                        id = 2,
                        eventType = "move",
                        lastEdited = null,
                        lastEditedBy = "",
                        mediaNodes = "",
                        userExceptions = "",
                        expansion = null,
                        disableEvent = false
                    };

                    EventModel trashData = new EventModel() {
                        id = 3,
                        eventType = "trash",
                        lastEdited = null,
                        lastEditedBy = "",
                        mediaNodes = "",
                        userExceptions = "",
                        expansion = null,
                        disableEvent = false
                    };

                    EventModel deleteData = new EventModel() {
                        id = 4,
                        eventType = "delete",
                        lastEdited = null,
                        lastEditedBy = "",
                        mediaNodes = "",
                        userExceptions = "",
                        expansion = null,
                        disableEvent = false
                    };

                    using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                        scope.Database.Save<EventModel>(saveData);
                        scope.Database.Save<EventModel>(moveData);
                        scope.Database.Save<EventModel>(trashData);
                        scope.Database.Save<EventModel>(deleteData);
                    }
                }
                catch(Exception ex) {
                    Logger.Error<EventModel>("Failed to save Media Protector initial settings",ex.Message);
                }

            }

        }
    }
}