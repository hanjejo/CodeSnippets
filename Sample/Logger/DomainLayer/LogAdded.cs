using CodeSnippets.EventAggregator;

namespace Sample.Logger.DomainLayer
{
    // 로그 추가 이벤트 정의
    // 로그 추가될 경우 돌아야하는 로직을 해당 이벤트를 참고하여 구현할 수 있다.
    public class LogAdded : Event
    {
    }
}
