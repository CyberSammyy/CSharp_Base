using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;
using System.Linq;

namespace Lesson7.Game
{
    public class Map
    {
        public int XSize { get; set; }
        public int YSize { get; set; }
        public int TurnCount { get; set; }
        public char WeaponInHand { get; set; } = ' ';
        public char ArmorOnPlayer { get; set; } = ' ';
        public char Armor { get; set; }
        private int enemyMovementsCount;
        Player player;
        Enemy[] enemies;
        Cell[,] cells;
        Random rand = new Random();
        ICarriable[] items;

        private int attempt_counter = 0;
        public Map(int size_x, int size_y, Player player, Enemy[] enemies, ICarriable[] items)
        {
            XSize = size_x;
            YSize = size_y;
            TurnCount = 0;
            cells = new Cell[XSize, YSize];
            this.player = player;
            this.enemies = enemies;
            this.items = items;
            enemyMovementsCount = 0;
        }
        public Map()
        {
            TurnCount = 0;
            XSize = YSize = 0;
            cells = null;
        }
        public void SetEnemy(Enemy enemy, ref Cell cell)
        {
            if (cell.CanMove)
            {
                cell.IsEnemyKeeper = true;
                cell.CellValue = enemy.Icon;
            }
            else
            {
                throw new Exception("CanMove = false");
            }
        }
        public void SetPlayer(Player player, ref Cell cell)
        {
            if (cell.CanMove)
            {
                cell.IsPlayerKeeper = true;
                cell.CellValue = player.Icon;
            }
            else
            {
                throw new Exception("CanMove = false");
            }
        }
        public int[] Generator()
        {
            Random rand1 = new Random((int)DateTime.Now.Day);
            Random rand2 = new Random((int)DateTime.Now.Day - (int)DateTime.Now.Second);
            int[] ret = new int[2];
            do
            {
                ret[0] = rand1.Next(-1, 1);
                ret[1] = rand2.Next(-1, 1);
            }
            while (ret[0] == ret[1]);
            return ret;
        }
        public (bool, int, int) IsPlayerNear(Enemy enemy)
        {
            bool res;
            int X, Y;
            if (cells[enemy.X + 0, enemy.Y - 1].IsPlayerKeeper)
            {
                res = true;
                X = enemy.X + 0;
                Y = enemy.Y - 1;
                var tuple = (res, X, Y);
                return tuple;
            }
            else if (cells[enemy.X - 1, enemy.Y + 0].IsPlayerKeeper)
            {
                res = true;
                X = enemy.X - 1;
                Y = enemy.Y + 0;
                var tuple = (res, X, Y);
                return tuple;
            }
            else if (cells[enemy.X + 0, enemy.Y + 1].IsPlayerKeeper)
            {
                res = true;
                X = enemy.X + 0;
                Y = enemy.Y + 1;
                var tuple = (res, X, Y);
                return tuple;
            }
            else if (cells[enemy.X + 1, enemy.Y + 0].IsPlayerKeeper) //if doesnt work replace with .CellValue == player.Icon
            {
                res = true;
                X = enemy.X + 1;
                Y = enemy.Y + 0;
                var tuple = (res, X, Y);
                return tuple;
            }
            else return (false, 0, 0);
        }
        /// <summary>
        /// Returning 2 values. 1 - X, 2 - Y (Potential positions to move - newX, newY)
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="enemy_index"></param>
        /// <returns></returns>
        public (int, int) ReturnNewPosition(int dx, int dy, int enemy_index)
        {
            var tuple = (enemies[enemy_index].X + dx, enemies[enemy_index].Y + dy);
            return tuple;
        }
        public int IndexOfNearestEnemy(Enemy[] enemies)
        {
            double[] distances = new double[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                distances[i] = CalculateDistance(enemies[i].X, enemies[i].Y);
            }
            return Array.IndexOf(distances, distances.Min());
        }
        public double CalculateDistance(int newX, int newY)
        {
            double ans = Sqrt((player.X - newX) * (player.X - newX) + (player.Y - newY) * (player.Y - newY));
            return ans;
        }
        public int ChooseIndex(bool[] indicators, int enemy_index) //0 - down, 1 - up, 2 - left, 3 - right
        {
            double[] movements = new double[4];
            for (int i = 0; i < 4; i++)
            {
                if (indicators[i] == true)
                {
                    if (i == 0)
                    {
                        double move_down = CalculateDistance(ReturnNewPosition(1, 0, enemy_index).Item1, ReturnNewPosition(1, 0, enemy_index).Item2);
                        movements[i] = move_down;
                    }
                    else if (i == 1)
                    {
                        double move_up = CalculateDistance(ReturnNewPosition(-1, 0, enemy_index).Item1, ReturnNewPosition(-1, 0, enemy_index).Item2);
                        movements[i] = move_up;
                    }
                    else if (i == 2)
                    {
                        double move_left = CalculateDistance(ReturnNewPosition(0, -1, enemy_index).Item1, ReturnNewPosition(0, -1, enemy_index).Item2);
                        movements[i] = move_left;
                    }
                    else if (i == 3)
                    {
                        double move_right = CalculateDistance(ReturnNewPosition(0, 1, enemy_index).Item1, ReturnNewPosition(0, 1, enemy_index).Item2);
                        movements[i] = move_right;
                    }
                }
                else
                {
                    movements[i] = 999999;
                }
            }
            return Array.IndexOf(movements, movements.Min());
        }
        public void MoveEnemy()
        {
            enemyMovementsCount++;
            if (attempt_counter >= 0)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    int index = rand.Next(0, 4);
                    bool[] indicators = new bool[4];
                    if (IsPositionAvailable_Enemy(1, 0, enemies[i])) //down
                    {
                        indicators[0] = true;
                    }
                    else { indicators[0] = false; attempt_counter++; }
                    if (IsPositionAvailable_Enemy(-1, 0, enemies[i])) //up
                    {
                        indicators[1] = true;
                    }
                    else { indicators[1] = false; attempt_counter++; }
                    if (IsPositionAvailable_Enemy(0, -1, enemies[i])) //left
                    {
                        indicators[2] = true;
                    }
                    else { indicators[2] = false; attempt_counter++; }
                    if (IsPositionAvailable_Enemy(0, 1, enemies[i])) //right
                    {
                        indicators[3] = true;
                    }
                    else { indicators[3] = false; attempt_counter++; }
                    if (indicators[0] == true || indicators[1] == true || indicators[2] == true || indicators[3] == true)
                    {
                        if (enemyMovementsCount % 2 == 0 && IsSeeingPlayer())
                        {
                            while (indicators[index] != true)
                            {
                                index = rand.Next(0, 4);
                            }
                        }
                        else index = ChooseIndex(indicators, i);
                        switch (index)
                        {
                            case 0:
                                SetEnemiesToPositions(1, 0, ref enemies[i]);
                                break;
                            case 1:
                                SetEnemiesToPositions(-1, 0, ref enemies[i]);
                                break;
                            case 2:
                                SetEnemiesToPositions(0, -1, ref enemies[i]);
                                break;
                            case 3:
                                SetEnemiesToPositions(0, 1, ref enemies[i]);
                                break;
                            default:
                                break;
                        }
                    }
                    if (IsPlayerNear(enemies[i]).Item1 && enemies[i].IsAlive && player.IsAlive)
                    {
                        player.HealthPoints -= enemies[i].Damage;
                    }
                }
            }
            else
            {

            }
        }

        private bool IsSeeingPlayer(Enemy enemy = null) // this method will be able to check, whether enemy can see player or not. Work in progress.
        {
            //if(player.X < enemy.X)
            //{
            //    for(int i = player.X; i < enemy.X; i++)
            //    {

            //    }
            //}
            //for(int i = 0; i < enemies.Length; i++)
            //{
            //    if(player.X > enemies[i].X)
            //    {
            //        for(int k = enemies[i].X; k < player.X; k++)
            //        {
            //            if(player.Y > enemies[i].Y)
            //            {
            //                for(int p = player.Y; p < enemies[i].Y; p++)
            //                {
            //                    if (cells[k, p].CellValue == '#')
            //                    {
            //                        return true;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                for (int p = enemies[i].Y; p < player.Y; p++)
            //                {
            //                    if (cells[k, p].CellValue == '#')
            //                    {
            //                        return true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int k = player.X ; k < enemies[i].X; k++)
            //        {
            //            if (player.Y > enemies[i].Y)
            //            {
            //                for (int p = player.Y; p < enemies[i].Y; p++)
            //                {
            //                    if (cells[k, p].CellValue == '#')
            //                    {
            //                        return true;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                for (int p = enemies[i].Y; p < player.Y; p++)
            //                {
            //                    if (cells[k, p].CellValue == '#')
            //                    {
            //                        return true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return true;
        }

        public void Move()
        {
            RenderMap();
            SetItemsToPositions(items);
            while (player.IsAlive)
            {
                bool isInputSuccess;
                do
                {
                    if (IsPositionAvailable(0, 0)) //default values - 0,0
                    {
                        SetPlayerToPosition(0, 0); //default values - 0,0
                    }
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (IsPositionAvailable_Enemy(0, 0, enemies[i]))
                        {
                            SetEnemiesToPositions(0, 0, ref enemies[i]);
                        }
                        else
                        {
                            for (int p = 1; p >= -1; p--)
                            {
                                bool flag = true;
                                for (int k = -1; k <= 1; k++)
                                {
                                    if (IsPositionAvailable_Enemy(p, k, enemies[i]))
                                    {
                                        SetEnemiesToPositions(p, k, ref enemies[i]);
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag == false) break;
                            }
                        }
                    }
                    RenderMap();
                    ConsoleKey key = Console.ReadKey().Key;
                    isInputSuccess = true;
                    switch (key)
                    {
                        case ConsoleKey.A:
                            {

                                if (IsPositionAvailable(0, -1) && player.IsAlive)
                                {
                                    for (int i = 0; i < enemies.Length; i++)
                                    {
                                        DamageToEnemy(0, -1, enemies[i]);
                                    }
                                    SetPlayerToPosition(0, -1);
                                    TurnCount++;
                                    player.Icon = 'P';
                                }
                                else
                                {
                                    if (!player.IsAlive)
                                    {
                                        player.Icon = 'X';
                                        Console.Clear();
                                        ToConsole("GAME OVER", ConsoleColor.Red);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Position is unavailable");
                                        for (int i = 0; i < enemies.Length; i++)
                                        {
                                            DamageToEnemy(0, -1, enemies[i]);
                                        }
                                    }
                                    isInputSuccess = false;
                                }
                                MoveEnemy();
                                break;
                            }
                        case ConsoleKey.W:
                            {
                                if (IsPositionAvailable(-1, 0) && player.IsAlive)
                                {
                                    for (int i = 0; i < enemies.Length; i++)
                                    {
                                        DamageToEnemy(-1, 0, enemies[i]);
                                    }
                                    SetPlayerToPosition(-1, 0);
                                    TurnCount++;
                                    player.Icon = '↑';
                                }
                                else
                                {
                                    if (!player.IsAlive)
                                    {
                                        player.Icon = 'X';
                                        Console.Clear();
                                        ToConsole("GAME OVER", ConsoleColor.Red);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Position is unavailable");
                                        for (int i = 0; i < enemies.Length; i++)
                                        {
                                            DamageToEnemy(-1, 0, enemies[i]);
                                        }
                                    }
                                    isInputSuccess = false;
                                }
                                MoveEnemy();
                                break;
                            }
                        case ConsoleKey.D:
                            {

                                if (IsPositionAvailable(0, 1) && player.IsAlive)
                                {
                                    for (int i = 0; i < enemies.Length; i++)
                                    {
                                        DamageToEnemy(0, 1, enemies[i]);
                                    }
                                    SetPlayerToPosition(0, 1);
                                    TurnCount++;
                                    player.Icon = '→';
                                }
                                else
                                {
                                    if (!player.IsAlive)
                                    {
                                        player.Icon = 'X';
                                        Console.Clear();
                                        ToConsole("GAME OVER", ConsoleColor.Red);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Position is unavailable");
                                        for (int i = 0; i < enemies.Length; i++)
                                        {
                                            DamageToEnemy(0, 1, enemies[i]);
                                        }
                                    }
                                    isInputSuccess = false;
                                }
                                MoveEnemy();
                                break;
                            }
                        case ConsoleKey.S:
                            {

                                if (IsPositionAvailable(1, 0) && player.IsAlive)
                                {
                                    for (int i = 0; i < enemies.Length; i++)
                                    {
                                        DamageToEnemy(1, 0, enemies[i]);
                                    }
                                    SetPlayerToPosition(1, 0);
                                    TurnCount++;
                                    player.Icon = '↓';
                                }
                                else
                                {
                                    if (!player.IsAlive)
                                    {
                                        player.Icon = 'X';
                                        Console.Clear();
                                        ToConsole("GAME OVER", ConsoleColor.Red);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Position is unavailable");
                                        for (int i = 0; i < enemies.Length; i++)
                                        {
                                            DamageToEnemy(1, 0, enemies[i]);
                                        }
                                    }
                                    isInputSuccess = false;
                                }
                                MoveEnemy();
                                break;
                            }
                        case ConsoleKey.I:
                            {
                                Console.Clear();
                                while (true)
                                {
                                    Console.WriteLine(player.inventory.items.Count);
                                    foreach (var item in player.inventory.items)
                                    {
                                        Console.WriteLine(item.Icon + " " + item.Name);
                                    }
                                    if(Console.ReadKey().Key == ConsoleKey.Escape)
                                    {
                                        break;
                                    }
                                    //else if()
                                }
                                break;
                            }
                        case ConsoleKey.D1:
                            {
                                if (WeaponInHand == ' ')
                                {
                                    foreach (var item in player.inventory.items)
                                    {
                                        if (item is Weapon)
                                        {
                                            WeaponInHand = item.Icon;
                                            player.Damage += item.AdditDamage;
                                            break;
                                        }
                                        //else if
                                    }
                                }
                                else
                                {
                                    WeaponInHand = ' ';
                                    foreach (var item in player.inventory.items)
                                    {
                                        if (item is Weapon)
                                        {
                                            WeaponInHand = ' ';
                                            player.Damage -= item.AdditDamage;
                                            break;
                                        }
                                        //else if
                                    }

                                }
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                Program.RenderPause();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong key, use \"W,A,S,D\"");
                                isInputSuccess = false;
                                break;
                            }
                    }
                    RenderMap();
                }
                while (!isInputSuccess);
            }
        }

        private void SetItemsToPositions(ICarriable[] items)
        {
            int counter = items.Length - 1;
            while(counter >= 0)
            {
                int x = rand.Next(0, 15);
                int y = rand.Next(0, 15);
                if(cells[x, y].CanMove)
                {
                    cells[x, y].CellValue = items[counter].Icon;
                    items[counter].X = x;
                    items[counter].Y = y;
                    cells[x, y].IsItemKeeper = true;
                    counter--;
                }
            }
        }

        public void CreateMap(char[,] symbols)
        {
            int size_x = symbols.GetLength(0);
            int size_y = symbols.GetLength(1);
            for (int i = 0; i < size_x; i++)
            {
                for (int j = 0; j < size_y; j++)
                {
                    cells[i, j] = new Cell(i, j, symbols[i, j], enemies[0]);
                }
            }
        }
        public void ToConsole(string str, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ResetColor();
        }
        public void SetEnemiesToPositions(int xDelta_Enemy, int yDelta_Enemy, ref Enemy enemy)
        {
            int xPos_Enemies = enemy.X;
            int yPos_Enemies = enemy.Y;

            int newPosX_Enemies = xPos_Enemies + xDelta_Enemy;
            int newPosY_Enemies = yPos_Enemies + yDelta_Enemy;
            if (enemy.IsAlive)
            {
                if (newPosX_Enemies < 0 || newPosY_Enemies < 0)
                {
                    newPosX_Enemies = newPosY_Enemies = 1;
                }
                if (xPos_Enemies == newPosX_Enemies && yPos_Enemies == newPosY_Enemies)
                {
                    SetEnemy(enemy, ref cells[newPosX_Enemies, newPosY_Enemies]);
                }
                else
                {
                    cells[xPos_Enemies, yPos_Enemies].Reset();
                    SetEnemy(enemy, ref cells[newPosX_Enemies, newPosY_Enemies]);
                    enemy.X = newPosX_Enemies;
                    enemy.Y = newPosY_Enemies;
                }
            }
            else
            {
                cells[xPos_Enemies, yPos_Enemies].Reset();
            }
        }
        public (bool, int, int) IsEnemyNear()
        {
            bool res;
            int X, Y;
            if (cells[player.X + 0, player.Y - 1].IsEnemyKeeper)
            {
                res = true;
                X = player.X + 0;
                Y = player.Y - 1;
                var tuple = (res, X, Y);
                return tuple;
            }
            else if (cells[player.X - 1, player.Y + 0].IsEnemyKeeper)
            {
                res = true;
                X = player.X - 1;
                Y = player.Y + 0;
                var tuple = (res, X, Y);
                return tuple;
            }
            else if (cells[player.X + 0, player.Y + 1].IsEnemyKeeper)
            {
                res = true;
                X = player.X + 0;
                Y = player.Y + 1;
                var tuple = (res, X, Y);
                return tuple;
            }
            else if (cells[player.X + 1, player.Y + 0].IsEnemyKeeper)
            {
                res = true;
                X = player.X + 1;
                Y = player.Y + 0;
                var tuple = (res, X, Y);
                return tuple;
            }
            else return (false, 0, 0);
        }
        public void DamageToEnemy(int dx, int dy, Enemy enemy)
        {
            if ((player.X + dx == enemy.X && player.Y + dy == enemy.Y) && IsEnemyNear().Item1 && player.IsAlive) enemy.HealthPoints -= player.Damage;
        }
        public void SetPlayerToPosition(int xDelta_Player, int yDelta_Player)
        {
            int xPos_Player = player.X;
            int yPos_Player = player.Y;

            int newPosX_Player = xPos_Player + xDelta_Player;
            int newPosY_Player = yPos_Player + yDelta_Player;

            cells[xPos_Player, yPos_Player].Reset();
            if (player.IsAlive)
                SetPlayer(player, ref cells[newPosX_Player, newPosY_Player]);
            else
            {
                player.Icon = 'X';
                SetPlayer(player, ref cells[xPos_Player, yPos_Player]);
            }

            player.X = newPosX_Player;
            player.Y = newPosY_Player;
        }
        public bool IsPositionAvailable_Enemy(int xDelta_Enemy, int yDelta_Enemy, Enemy enemy)
        {
            int xPos_Enemy = enemy.X;
            int yPos_Enemy = enemy.Y;

            bool isInBounds = true;
            int newPosX_Enemy = xPos_Enemy + xDelta_Enemy;
            int newPosY_Enemy = yPos_Enemy + yDelta_Enemy;

            if (xPos_Enemy + xDelta_Enemy < 0 || xPos_Enemy + xDelta_Enemy >= cells.GetLength(0) ||
                yPos_Enemy + yDelta_Enemy < 0 || yPos_Enemy + yDelta_Enemy >= cells.GetLength(1))
            {
                isInBounds = false;
            }
            if (isInBounds && cells[newPosX_Enemy, newPosY_Enemy].IsItemKeeper)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (newPosX_Enemy == items[i].X && newPosY_Enemy == items[i].Y)
                    {

                    }
                }
            }
            return isInBounds && cells[newPosX_Enemy, newPosY_Enemy].CanMove && !cells[newPosX_Enemy, newPosY_Enemy].IsPlayerKeeper;
        }
        public bool IsPositionAvailable(int xDelta_Player, int yDelta_Player)
        {
            int xPos_Player = player.X;
            int yPos_Player = player.Y;

            bool isInBounds = true;
            int newPosX_Player = xPos_Player + xDelta_Player;
            int newPosY_Player = yPos_Player + yDelta_Player;


            if (xPos_Player + xDelta_Player < 0 || xPos_Player + xDelta_Player >= cells.GetLength(0) ||
                yPos_Player + yDelta_Player < 0 || yPos_Player + yDelta_Player >= cells.GetLength(1))
            {
                isInBounds = false;
            }
            if(isInBounds && cells[newPosX_Player, newPosY_Player].IsItemKeeper)
            {
                for(int i = 0; i < items.Length; i++)
                {
                    if(newPosX_Player == items[i].X && newPosY_Player == items[i].Y)
                    {
                        player.inventory.items.Add(items[i]);
                        cells[newPosX_Player, newPosY_Player].ResetItem();
                    }
                }
            }
            if (isInBounds && cells[newPosX_Player, newPosY_Player].CellValue == 'H') //i will replace just letters with objects later
            {
                player.HealthPoints += 40;
                cells[newPosX_Player, newPosY_Player].CellValue = ' ';
            }
            if (isInBounds && cells[newPosX_Player, newPosY_Player].CellValue == 'F') //i will replace just letters with objects later
            {
                Console.Clear();
                ToConsole("YOU WON!! Press any key to exit...", ConsoleColor.Green);
                if(Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Environment.Exit(0);
                }
                
            }
            return isInBounds && cells[newPosX_Player, newPosY_Player].CanMove && !cells[newPosX_Player, newPosY_Player].IsEnemyKeeper;
        }
        public void RenderMap()
        {
            Console.Clear();
            Console.WriteLine($"Turn count: {TurnCount}, HP: {player.HealthPoints}, Nearest enemy HP: {enemies[IndexOfNearestEnemy(enemies)].HealthPoints}, Player's damage: {player.Damage}");
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                Console.WriteLine(new string('-', cells.GetLength(1) * 4 + 1));
                Console.Write("|");
                for (int k = 0; k < cells.GetLength(1); k++)
                {
                    if (cells[i, k].IsPlayerKeeper)
                    {
                        ToConsole(cells[i, k].ToString().TrimEnd(), ConsoleColor.Yellow);
                        Console.Write(WeaponInHand);
                        Console.Write("|");
                    }
                    else
                    {
                        if (cells[i, k].CellValue == '#')
                        {
                            ToConsole(cells[i, k].ToString(), ConsoleColor.White);
                            Console.Write("|");
                        }
                        else if (cells[i, k].CellValue == '@')
                        {
                            ToConsole(cells[i, k].ToString(), ConsoleColor.Magenta);
                            Console.Write("|");
                        }
                        else if (cells[i, k].CellValue == 'E')
                        {
                            ToConsole(cells[i, k].ToString(), ConsoleColor.Red);
                            Console.Write("|");
                        }
                        else if (cells[i, k].CellValue == 'A' || cells[i, k].CellValue == 'H')
                        {
                            ToConsole(cells[i, k].ToString(), ConsoleColor.Blue);
                            Console.Write("|");
                        }
                        else
                        {
                            Console.Write($"{cells[i, k]}|");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', cells.GetLength(1) * 4 + 1));
        }
    }
}
