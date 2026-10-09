public class RatingRecord
{
    public int RecordId { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int Rating { get; set; }
    public long Timestamp { get; set; }


    public RatingRecord(int id, int userId, int movieId, int rating, long timestamp)
    {
        RecordId = id;
        UserId = userId;
        MovieId = movieId;
        Rating = rating;
        Timestamp = timestamp;
    }
  
}