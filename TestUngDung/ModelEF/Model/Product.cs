namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }
       
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phấm")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá sản phẩm")]
        public decimal? UniCost { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập số lượng sản phẩm")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập hình ảnh sản phẩm")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mô tả sản phẩm")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tình trạng sản phẩm")]
        [StringLength(50)]
        public string Status { get; set; }
        
        public virtual Category Category { get; set; }
    }
}
