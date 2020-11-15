using System;
using System.Text.RegularExpressions;

namespace FoodWarning.Portable.Models
{
  public class RSSFeedItem
  {



    public string Description { get; set; }
    public string Link { get; set; }
    public string PublishDate { get; set; }
    public string ImageURL { get; set; }
    public int Id { get; set; }

    private string title;
    public string Title
    {
      get
      {
        return title;
      }
      set
      {
        title = value;
        //RSS feed always is "Author : Title", split it here and set correctly
        var splitIndex = title.IndexOf(":", StringComparison.OrdinalIgnoreCase);
        if (splitIndex > -1)
        {
          title = title.Substring(splitIndex + 1, title.Length - splitIndex - 1).Trim();
        }
      }
    }

    private string caption;

    public string Caption
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(caption))
          return caption;

        //get rid of HTML tags
        caption = Regex.Replace(Description, "<[^>]*>", string.Empty);

        //get rid of multiple blank lines
        caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

        return caption;
      }
    }

    private bool showImage = true;

    public bool ShowImage
    {
      get { return showImage; }
      set { showImage = value; }
    }

    private string image = "";

    /// <summary>
    /// When we set the image, mark show image as true
    /// </summary>
    public string Image
    {
      get { return image; }
      set
      {
        image = value;
        showImage = true;
      }

    }

  }
}
