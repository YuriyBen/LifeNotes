using System;
using System.Collections.Generic;

namespace LifeNotes.Entities
{
    public partial class TblRefreshToken
    {
        public int TokenId { get; set; }
        public long UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual Users User { get; set; }
    }
}
