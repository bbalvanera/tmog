using System;

namespace TMog.Entities
{
    public class WorldQuestsSubmissionLog
    {
        public int WorldQuestsSubmissionLogId { get; set; }

        public DateTime SubmissionDate { get; set; }

        // total records submitted
        public int SubmitCount { get; set; }

        // total records that were new with respect of current records where expiresOn <> from submitted.
        public int ProcessCount { get; set; }
    }
}
