using System;
using SFML.Window;

public class Grid
{
    private bool[,] _grid;
    private Random _rand;
    private int _sizeX;
    private int _sizeY;

    public Grid(Random rand, int sizeX, int sizeY)
    {
        if (sizeX < 2)
            throw new ArgumentOutOfRangeException(sizeX.ToString());
        if (sizeY < 2)
            throw new ArgumentOutOfRangeException(sizeY.ToString());
        _rand = rand;
        _sizeX = sizeX;
        _sizeY = sizeY;
        _grid = new bool[_sizeX, _sizeY];
        for (int y = 0; y < _sizeY; y++) {
            for (int x = 0; x < _sizeX; x++) {
                _grid[x, y] = false;
            }
        }
    }

    public bool getCell(int x, int y)
    {
        return _grid[x, y];
    }

    public void generateMaze()
    {
        Vector2i cN = new Vector2i();
        Vector2i cS = new Vector2i();
        int intDir, intDone = 0;
        bool blnBlocked = false;
        Vector2i[] cDir = new Vector2i[4];
        for (int x = 0; x < 4; x++) {
            cDir[x].X = 0;
            cDir[x].Y = 0;
        }
        do {
            cS.X = 1 + _rand.Next(_sizeX / 2) * 2;
            cS.Y = 1 + _rand.Next(_sizeY / 2) * 2;
            if (intDone == 0)
                _grid[cS.X, cS.Y] = true;
            if (_grid[cS.X, cS.Y] == true) {
                do {
                    randomDirections(cDir);
                    blnBlocked = true;
                    for (intDir = 0; intDir < 4; intDir++) {
                        cN.X = cS.X + cDir[intDir].X * 2;
                        cN.Y = cS.Y + cDir[intDir].Y * 2;
                        if (isFree(cN)) {
                            _grid[cN.X, cN.Y] = true;
                            _grid[cS.X + cDir[intDir].X, cS.Y + cDir[intDir].Y] = true;
                            cS.X = cN.X;
                            cS.Y = cN.Y;
                            blnBlocked = false;
                            intDone = intDone + 1;
                            break;
                        }
                    }
                } while (blnBlocked == false);
            }
        } while ((intDone + 1) < ((_sizeX - _sizeX % 2) * (_sizeY - _sizeY % 2) / 4));
    }

    private bool isFree(Vector2i cF)
    {
        if (cF.X < _sizeX && cF.X > 0 && cF.Y < _sizeY && cF.Y > 0)
            if (_grid[cF.X, cF.Y] == false)
                return true;
        return false;
    }

    private void randomDirections(Vector2i[] cDir)
    {
        for (int x = 0; x < 4; x++) {
            cDir[x].X = 0;
            cDir[x].Y = 0;
        }
        switch (_rand.Next(4)) {
            case 0:
                cDir[0].X = -1;
                cDir[1].X = 1;
                cDir[2].Y = -1;
                cDir[3].Y = 1;
                break;

            case 1:
                cDir[3].X = -1;
                cDir[2].X = 1;
                cDir[1].Y = -1;
                cDir[0].Y = 1;
                break;

            case 2:
                cDir[2].X = -1;
                cDir[3].X = 1;
                cDir[0].Y = -1;
                cDir[1].Y = 1;
                break;

            case 3:
                cDir[1].X = -1;
                cDir[0].X = 1;
                cDir[3].Y = -1;
                cDir[2].Y = 1;
                break;
        }
    }
}