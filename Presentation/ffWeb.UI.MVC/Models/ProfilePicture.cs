using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace ffWeb.UI.MVC.Models
{
      public class ProfilePictureModel
        {
            [UIHint("ProfileImage")]
            public string ImageUrl { get; set; }
        }

        public class EditorInputModel
        {
            public ProfilePictureModel Profile { get; set; }
            public double Top { get; set; }
            public double Bottom { get; set; }
            public double Left { get; set; }
            public double Right { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }
    }
