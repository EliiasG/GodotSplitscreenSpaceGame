using Godot;
using System;

public interface ILifeCountedGameMode
{
    int Player1Lives{ get; }
    int Player2Lives { get; }
}
