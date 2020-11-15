using Android.App;
using Android.Views;
using Android.Widget;
using FoodWarning.Droid.PlatformSpecific;
using FoodWarning.Portable.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodWarning.Droid.Adapters
{
    public class FeedItemAdapter : BaseAdapter
    {
        private ImageLoader imageLoader;
        private Activity activity;
        IEnumerable<RSSFeedItem> items;
        public FeedItemAdapter(Activity activity, IEnumerable<RSSFeedItem> items)
        {
            this.imageLoader = new ImageLoader(activity);
            this.activity = activity;
            this.items = items;
        }

        //Wrapper class for adapter for cell re-use
        private class FeedItemAdapterHelper : Java.Lang.Object
        {
            public TextView Title { get; set; }
            public TextView Caption { get; set; }
            public ImageView Image { get; set; }
        }




        #region implemented abstract members of BaseAdapter
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            var item = items.ElementAt(position);
            return item.Id;
        }

        public override bool HasStableIds
        {
            get { return true; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            FeedItemAdapterHelper helper;
            if (convertView == null)
            {
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.RSSItem, null);
                helper = new FeedItemAdapterHelper
                {
                    Title = convertView.FindViewById<TextView>(Resource.Id.text_title),
                    Caption = convertView.FindViewById<TextView>(Resource.Id.text_caption),
                    Image = convertView.FindViewById<ImageView>(Resource.Id.image)
                };
                helper.Caption.SetMaxLines(2);
                helper.Title.SetMaxLines(2);
                convertView.Tag = helper;
            }
            else
            {
                helper = convertView.Tag as FeedItemAdapterHelper;
            }

            var item = items.ElementAt(position);
            helper.Title.Text = item.Title;
            helper.Caption.Text = item.Caption;
            try
            {
                helper.Caption.Text = helper.Caption.Text.Split("Grund der Warnung:\n")[1];
            }
            catch (Exception)
            {
            }
            helper.Image.Visibility = item.ShowImage ? ViewStates.Visible : ViewStates.Gone;
            if (item.ShowImage)
            {
                imageLoader.DisplayImage(item.Image, helper.Image, Resource.Drawable.ic_launcher);
            }

            return convertView;
        }

        public override int Count
        {
            get
            {
                return items.Count();
            }
        }
        #endregion
    }
}