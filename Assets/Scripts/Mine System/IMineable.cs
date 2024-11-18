public interface IMineable
{
    void Mine();
    bool IsDepleted { get; }
    void Respawn();
}