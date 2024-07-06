public interface IDebugger{
    void Log(string message);
    void Warning(string message);
    void Error(string message);
}