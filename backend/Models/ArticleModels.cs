using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Article
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("article_name")]
        public int ArticleName { get; set; }

        [Column("article_count")]
        public int ArticleCount { get; set; }

    }

    public class ArticleUpdate
    {
        [Required]
        public required int ArticleAQuantity { get; set; }

        [Required]
        public required int ArticleBQuantity { get; set; }
    }
}
