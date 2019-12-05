using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace MediaProtector.App_Plugins.MediaProtector.Models {
    [TableName("MediaProtector")]
    [PrimaryKey("id",AutoIncrement = false)]
    [ExplicitColumns]
    public class EventModel {
        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        public int id { get; set; }

        [Column("eventType")]
        public string eventType { get; set; }

        [Column("mediaNodes")]
        public string mediaNodes { get; set; }

        [Column("userExceptions")]
        public string userExceptions { get; set; }

        [Column("lastEdited")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? lastEdited { get; set; }

        [Column("lastEditedBy")]
        public string lastEditedBy { get; set; }

        [Column("expansion")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string expansion { get; set; }
    }
}