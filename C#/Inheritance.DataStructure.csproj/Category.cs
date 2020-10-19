using System;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        public string ProductName { get; set; }
        public MessageType MessageType { get; set; }
        public MessageTopic MessageTopic { get; set; }


        public Category(string productName, MessageType messageType, MessageTopic messageTopic)
        {
            ProductName = productName;
            MessageType = messageType;
            MessageTopic = messageTopic;
        }

        public int CompareTo(object obj) // исправить этот метод
        { 
            var category = obj as Category;

            if (category == null || ProductName == null)
                return 0;

            if (ProductName.CompareTo(category.ProductName) == 0)
            {
                if ((int)MessageType < (int)category.MessageType)
                    return -1;
                else if ((int)MessageType > (int)category.MessageType)
                    return 1;

                if ((int)MessageTopic < (int)category.MessageTopic)
                    return -1;
                else if ((int)MessageTopic > (int)category.MessageTopic)
                    return 1;
                else
                    return 0;
            }

            return ProductName.CompareTo(category.ProductName);
        }

        public override bool Equals(object obj)
        {
            var category = obj as Category;

            if (category == null)
                return false;

            return ProductName == category.ProductName
                && MessageType == category.MessageType
                && MessageTopic == category.MessageTopic;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}", ProductName, MessageType, MessageTopic);
        }

        public static bool operator < (Category category1, Category category2)
        {
            return category1.CompareTo(category2) == -1;
        }

        public static bool operator > (Category category1, Category category2)
        {
            return category1.CompareTo(category2) == 1;
        }

        public static bool operator <= (Category category1, Category category2)
        {
            return category1 == null || category1.CompareTo(category2) < 1;
        }

        public static bool operator >= (Category category1, Category category2)
        {
            return category1 == null || category1.CompareTo(category2) > -1;
        }
    }
}
