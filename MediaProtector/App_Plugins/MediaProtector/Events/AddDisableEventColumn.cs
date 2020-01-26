using MediaProtector.App_Plugins.MediaProtector.Models;
using System;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Scoping;

namespace MediaProtector.App_Plugins.MediaProtector.Events {
    public class AddDisableEventColumn:MigrationBase {

        private readonly IScopeProvider _scopeProvider;
        public AddDisableEventColumn(IMigrationContext context,IScopeProvider scopeProvider) : base(context) {
            _scopeProvider = scopeProvider;
        }
        public override void Migrate() {
            try{
                if(TableExists("MediaProtector")) {
                    if(!ColumnExists("MediaProtector","disableEvent")){
                        Alter.Table("MediaProtector").AddColumn("disableEvent").AsBoolean().WithDefaultValue(false).Do();
                    }                    
                }
                else{
                    Logger.Error<EventModel>("Failed to add disableEvent column to Media Protector table because the table doesn't exists ");
                }
            }
            catch(Exception ex){
                Logger.Error<EventModel>("Failed to add disableEvent column to Media Protector table ",ex.Message);
            }                      
        }
    }
}