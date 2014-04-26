using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;

namespace MyRoguelike
{
    internal class Game
    {
        private Sprite[,] _dungeonSprTiles;
        private Dungeon _m_dungeon;
        private Random _m_rand;
        private Stopwatch _stop;
        private Texture _textureStoneFloor;
        private Texture _textureStoneWall;
        private Texture _textureUnused;
        private View _view;
        private RenderWindow _win;
        private int DUNGEONSIZE_X = 41;
        private int DUNGEONSIZE_Y = 31;

        public void load()
        {
        }

        public void loop()
        {
            _stop = Stopwatch.StartNew(); //Start when the actual game is running
            while (_win.IsOpen()) {
                updateLogic();
                _win.DispatchEvents();
                _win.Clear(Color.White);
                for (int y = 0; y < DUNGEONSIZE_Y; y++) {
                    for (int x = 0; x < DUNGEONSIZE_X; x++) {
                        _win.Draw(_dungeonSprTiles[x, y]);
                    }
                }
                _win.Display();
            }
        }

        public void start()
        {
            _m_rand = new Random();
            _m_dungeon = new Dungeon(_m_rand, DUNGEONSIZE_X, DUNGEONSIZE_Y);

            _textureUnused = new Texture("dqtiles.png", new IntRect(64, 480, 32, 32));
            _textureStoneFloor = new Texture("dqtiles.png", new IntRect(0, 480, 32, 32));
            _textureStoneWall = new Texture("dqtiles.png", new IntRect(160, 512, 32, 32));

            _dungeonSprTiles = new Sprite[DUNGEONSIZE_X, DUNGEONSIZE_Y];
            for (int y = 0; y < DUNGEONSIZE_Y; y++) {
                for (int x = 0; x < DUNGEONSIZE_X; x++) {
                    _dungeonSprTiles[x, y] = new Sprite();
                    _dungeonSprTiles[x, y].Position = new Vector2f(x * 32, y * 32);
                    switch (_m_dungeon.getCell(x, y)) {
                        case Tile.StoneFloor:
                            _dungeonSprTiles[x, y].Texture = _textureStoneFloor;
                            break;

                        case Tile.StoneWall:
                            _dungeonSprTiles[x, y].Texture = _textureStoneWall;
                            break;

                        default:
                            _dungeonSprTiles[x, y].Texture = _textureUnused;
                            break;
                    }
                }
            }

            _win = new RenderWindow(new VideoMode(640, 480), "My window");
            _win.SetVerticalSyncEnabled(true);
            _win.SetVisible(true);
            _win.Closed += new EventHandler(OnClosed);

            _view = new View(new FloatRect(0.0f, 0.0f, 32 * DUNGEONSIZE_X, 32 * DUNGEONSIZE_Y));
            _win.SetView(_view);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            _win.Close();
        }

        private void updateLogic()
        {
            _stop.Restart();
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Game prog = new Game();
            prog.start();
            prog.load();
            prog.loop();
        }
    }
}