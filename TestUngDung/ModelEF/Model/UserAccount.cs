namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên tài khoản")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [StringLength(100)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập trạng thái")]
     
        public bool Status { get; set; }
    }
}
