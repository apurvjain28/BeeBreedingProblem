//Authors: Gowridevi Akasapu, Anju Thomas, Apurva Jain, Aditya Gupta, Emmanuel Kevin, Ravali Chilucoti, Amritpal Singh
//Copy Rights:      Conestoga College
//Group Number:     4

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeBreeding
{
    public class VM : INotifyPropertyChanged
    {
        #region Singleton
        private static VM vm;

        public static VM Instance { get { vm ??= new VM(); return vm; } }
        #endregion

        #region Constants
        public const int INVALID_INPUT_DISTANCE = -2;
        public const int EQUAL_DISTANCE = -1;
        #endregion

        #region Properties

        private int maggotA;
        public int MaggotA
        {
            get => maggotA;
            set { maggotA = value; propertyChanged(); }
        }

        private int maggotB;
        public int MaggotB
        {
            get => maggotB;
            set { maggotB = value; propertyChanged(); }
        }

        private int distance;
        public int Distance
        {
            get => distance;
            set { distance = value; propertyChanged(); }
        }
        #endregion

        #region Methods

        // This method returns the coordinates of the maggot cell based on cell number.
        private int[] CalcCoordinates(int maggotCellNumber)
        {
            // Cells are spiralling up. So the level of spiral is a pattern closely related to concentric circles.
            int levelOfSpiral = 0;

            // LevelOfSpiral can be calculated by using equation 3*n*(n-1)+1.
            while (3 * levelOfSpiral * (levelOfSpiral - 1) + 1 < maggotCellNumber)
            {
                levelOfSpiral++;
            }

            // PreSpiralLastCell is just the last cell number one level before the current level of spiral.
            int preSpiralLastCell = 3 * (levelOfSpiral - 1) * (levelOfSpiral - 2) + 1;

            // Position of the cell with respect to the cell where preSpiralLastCell is ending.
            double positionInCircle = maggotCellNumber == 1 ? 0 : maggotCellNumber - preSpiralLastCell;

            // finding x,y,z of cubical coordinate system using positionInCircle and levelOfSpiral
            int x = 0;
            int y = 0;
            int z = 0;

            // If one spiral rotation is divided in 6 equal groups, we see that cells in each blockgroup equals (levelofSpiral - 1).
            int cellsEachGroup = levelOfSpiral - 1;

            // Formula to calculate blockGroup number based on position in circle and cells in each group.
            int blockGroup = (int)Math.Ceiling(positionInCircle / cellsEachGroup);

            // Position in block group is the number at which the cell falls in a particular blockGroup.
            int posInBlockGroup = (int)(positionInCircle % cellsEachGroup != 0 ? positionInCircle % cellsEachGroup : cellsEachGroup);


            // Blockgroup 1,4 & 2,5 & 3,6 are diagonally opposite to each other from centroid.
            // So any cell at equal distance from centroid in diagonally opposite groups will have reverse co-ordinate location. 
            if (blockGroup == 0)
            {
                x = y = z = 0;
            }
            if (blockGroup == 1)
            {
                z = levelOfSpiral - 1;
                x = -posInBlockGroup;
                y = -x - z;
            }
            if (blockGroup == 2)
            {
                x = -(levelOfSpiral - 1);
                y = posInBlockGroup;
                z = -x - y;
            }
            if (blockGroup == 3)
            {
                y = levelOfSpiral - 1;
                z = -posInBlockGroup;
                x = -z - y;
            }
            if (blockGroup == 4)
            {
                z = -(levelOfSpiral - 1);
                x = posInBlockGroup;
                y = -x - z;
            }
            if (blockGroup == 5)
            {
                x = levelOfSpiral - 1;
                y = -posInBlockGroup;
                z = -x - y;
            }
            if (blockGroup == 6)
            {
                y = -(levelOfSpiral - 1);
                z = posInBlockGroup;
                x = -z - y;
            }

            int[] coordinates = new int[]
            {
                x,
                y,
                z
            };

            return coordinates;
        }

        // This method calculates the distance between the maggot cells A and B based on coordinates using coordinate geometry.
        public void CalcDistance()
        {
            // update distance to -1 when (i.e MaggotA = MaggotB)
            if (maggotA == maggotB)
            {
                Distance = EQUAL_DISTANCE;
                return;
            }

            // convert the maggot cells numbered as 1,2,3....10000 into x,y,z coordinates.
            int[] cellA = CalcCoordinates(MaggotA);
            int[] cellB = CalcCoordinates(MaggotB);

            // final distance calculation based on coordinate geometry.
            int finalDistance = (Math.Abs(cellA[0] - cellB[0]) + Math.Abs(cellA[1] - cellB[1]) + Math.Abs(cellA[2] - cellB[2])) / 2;
            Distance = finalDistance;
        }

        // Resets the fields to initial value.
        public void Reset()
        {
            MaggotA = MaggotB = Distance = 0;
        }

        // In case of invalid input, update the distance.
        public void Error()
        {
            Distance = INVALID_INPUT_DISTANCE;
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void propertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
