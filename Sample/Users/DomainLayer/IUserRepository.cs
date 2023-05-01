namespace Sample.Users.DomainLayer
{
    // 유저 리포지토리 인터페이스
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(User user);
        User Get(string username);
    }
}
