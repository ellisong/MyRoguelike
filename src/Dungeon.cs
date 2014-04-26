using System;

public enum Tile
{
    Unused = 0,
    StoneFloor,
    StoneWall,
    Door,
    Downstairs,
    Chest
}

public class Dungeon
{
    private Tile[,] _cells;
    private Random _rand;
    private int _sizeX;
    private int _sizeY;
    private Grid _grid;

    public Dungeon(Random rand, int sizeX, int sizeY)
    {
        if (sizeX < 3)
            throw new ArgumentOutOfRangeException(sizeX.ToString());
        if (sizeY < 3)
            throw new ArgumentOutOfRangeException(sizeY.ToString());

        _rand = rand;
        _sizeX = sizeX;
        _sizeY = sizeY;

        _cells = new Tile[_sizeX, _sizeY];
        _grid = new Grid(_rand, _sizeX, _sizeY);
        _grid.generateMaze();

        for (int y = 0; y < _sizeY; y++) {
            for (int x = 0; x < _sizeX; x++) {
                if (_grid.getCell(x, y) == false)
                    _cells[x, y] = Tile.StoneWall;
                else
                    _cells[x, y] = Tile.StoneFloor;
            }
        }
    }

    public Tile getCell(int x, int y)
    {
        return _cells[x, y];
    }
}