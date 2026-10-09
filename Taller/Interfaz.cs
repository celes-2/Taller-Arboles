public interface IRatingTree
{
 int Count { get; }
 bool Insert(RatingRecord value);
 RatingRecord? FindById(int recordId);
 bool RemoveById(int recordId);
 RatingRecord[] InOrder();
 RatingRecord[] PreOrder();
 RatingRecord[] PostOrder();
}
