using System;
using System.Collections.Generic;

public partial class CaptureHandler
{
    public double CaptureTime { get; }

    public Player Holder { get; set; }

    private Dictionary<Player, double> _playerTimes = new Dictionary<Player, double>();

    public event Action<Player> PlayerWon;

    public CaptureHandler(double captureTime)
    {
        CaptureTime = captureTime;
    }

    public void Update(double delta)
    {
        if (Holder == null) return;

        if (!_playerTimes.TryGetValue(Holder, out double time))
        {
            time = 0;
        }

        time += delta;

        if (time >= CaptureTime)
        {
            PlayerWon?.Invoke(Holder);
        }

        _playerTimes[Holder] = time;
    }

    public double GetTime(Player player)
    {
        return _playerTimes.ContainsKey(player) ? _playerTimes[player] : 0;
    }
}
