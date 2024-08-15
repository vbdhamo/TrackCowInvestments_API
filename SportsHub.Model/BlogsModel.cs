using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model
{
   public class BlogsModel
    {
    }

    #region Blogs New
    public class Blogs : DLC_MAIN
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string Labels { get; set; }
        public int IsReaderComment { get; set; }
        public int Status { get; set; }
        public string BlogMessage { get; set; }
        public string BlogImage { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int IsAdmin { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public string ReplyText { get; set; }
        public int ParentReplyId { get; set; }
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public string Search { get; set; }
        public int ReplyId { get; set; }
        public string BlogIds { get; set; }
        public string ImageName { get; set; }
        public string Remarks { get; set; }
        public Int64 TenantCode { get; set; }
    }
    #endregion

    #region Comments New
    public class Comments : DLC_MAIN
    {

        public int ReplyId { get; set; }
        public string ReplyText { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int BlogId { get; set; }
        public int ParentReplyId { get; set; }
        public int IsAdmin { get; set; }
        public int Status { get; set; }

    }
    #endregion
}
