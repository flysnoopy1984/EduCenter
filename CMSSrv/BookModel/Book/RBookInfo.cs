using System;
using System.Collections.Generic;
using System.Text;

namespace CMSSrv.BookModel.Book
{
    public class RBookInfo: BaseBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageThumbUrl { get; set; }

        public string Author { get; set; }
        public string Translator { get; set; }

        public string Description { get; set; }

        public string PublishUserId { get; set; }

        public string PublishUserName { get; set; }

        public string PublishUserHeaderUrl { get; set; }

        public string PublishUserTitle { get; set; }

        public int? ProductCategoryId { get; set; }

        public string DetailUrl { get; set; }


    }
}
