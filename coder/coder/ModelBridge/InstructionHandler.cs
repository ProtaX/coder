namespace coder.ModelBridge
{
    class InstructionHandler
    {
        public delegate void MoveEvent(string direction, bool positive);
        public delegate void StopEvent();

        public event MoveEvent SendMove;
        public event StopEvent SendStop;

        public void Move(string direction, bool positive)
        {
            SendMove?.Invoke(direction, positive);
        }

        public void Stop()
        {
            SendStop?.Invoke();
        }
    }
}
