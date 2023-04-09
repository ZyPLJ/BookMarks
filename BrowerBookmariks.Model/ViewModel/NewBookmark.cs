using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.ViewModel
{
    public class NewBookmark
    {
        public string Name { get; }
        public string Url { get; }
        public int Children { get; }
        public string Guid { get; }
        public int Id { get; }
        public string Item { get; }
        public bool IsTop { get; }

        public NewBookmark(string name, string url, int children, string guid, int id, string item, bool isTop)
        {
            Name = name;
            Url = url;
            Children = children;
            Guid = guid;
            Id = id;
            Item = item;
            IsTop = isTop;
        }

        public override bool Equals(object? obj)
        {
            return obj is NewBookmark other &&
                   Name == other.Name &&
                   Url == other.Url &&
                   Children == other.Children &&
                   Guid == other.Guid &&
                   Id == other.Id &&
                   Item == other.Item &&
                    IsTop == other.IsTop;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Url, Children, Guid, Id, Item);
        }
    }
}
