//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Studio.DotNet.Domain.T4
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblUserTblArticle
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
    
        public virtual TblArticle TblArticle { get; set; }
        public virtual TblUser TblUser { get; set; }
    }
}
