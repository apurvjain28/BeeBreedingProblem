# BeeBreedingProblem
Solving ACM-ICPC 5208 Bee Breeding Problem

Problem pdf: https://icpcarchive.ecs.baylor.edu/external/52/5208.pdf

Explanation with JS Code:

// The main part of our solution is to convert the cells numbered as 1,2,3....10000 into x,y,z coordinates (these coordinate system can be different, explained later)
//  Once coordinates of two cells are calculated, we can easily figure out the distance between the two cells, using coordinate geometry.

// Major Problem we are facing: Cells are spiralling up
// But advantage is spiral is a pattern too. And they are closely related to concentric circles.

// We will try to convert spirally numbered cells into x,y,z coordinates using two things:
// 1. Which level the cell is at. (eg: Cell 1 is at 1st level, Cell 2 to Cell 7 at 2nd level, Cell 8 to 19 at 3rd level and Cell 20 to 37 at 4th level.... and so on)
// Same as the number of circle in concentric circles keeping origin as 1st circle
// 2. And the position of the cell with respect to the cell where preultimate Spiral is ending. (preultimate Spiral: spiral just one level before the spiral of our cell)
// eg: Position of Cell 11 would be: 11 (cell no.) - 7 (since 11 is at 3rd level and preultimate level to 3rd level i.e. 2nd level is ending at 7)

// If we know level and position, now we can imagine all the cells lying in concentric circles form. 
//1. Cell 1 acts as origin
//2. Level acts as the diameter of the circle 
//3. Position: like a point somewhere in circle (considering starting point is cell where new spiral originates)  

// Problem doesnt end here: I told you I took cube coordinates and it is a 3D representation. 
// so we need to calculated x,y,z coordinates.

// Now we divide our circle circumference into 6 equal archs (we call these 6 archs as blockGroup)
// cellsEachGroup: Number of cells lying on each arch. Depending on the level we are at, the number of cells in each blockGroup will vary.
// eg: cellsEachGroup: At Level 3, cellsEachGroup will be 2 (i.e. Cells 8,9 lies at blockGroup 1; Cells 10,11 lies at blockGroup 2 and so on...)
// eg: cellsEachGroup: At Level 4, cellsEachGroup will be 3 (i.e. Cell 20,21,22 lies at blockGroup 1; Cell 23,24,25 lies at blockGroup 2; Cell 26,27,28 lies at BlockGroup3)

// The blockGroup of a cell (whether it lies on blockGroup 1 or blockGroup 2 ...... or blockGroup 6) will depend upon its position and cellsEachGroup

// eg1: lets suppose cell no. 10:
//                 It lies at level 3, 
//                 There are 12 total cells at level 3 , so 12/6 i.e. 2 cells in each blockGroup at level 3
//                 Now Cell 8 and Cell 9 lies at blockGroup 1; Cell 10 and Cell 11 lies at blockGroup 2 and so on....
// eg2: lets suppose cell no. 26:
//                  It lies at level 4,
//                  There are 18 total cells at level 4, so 18/6 i.e. 3 cells in each blockGroup at level 4
//                  Now Cell 20,21,22 lies at blockGroup 1; Cell 23,24,25 lies at blockGroup 2; Cell 26,27,28 lies at BlockGroup3 and so on....

// Now, blockNumber: is no. of cell at that blockGroup.
// eg1: lets suppose cell no. 10 again:
//                  Cell 10,11 lies at a blockGroup2; Cell 10 would be blockNumber 1, Cell 11 would be blockNumber 2
// eg2: lets suppose cell no. 26:
//                  Cell 26,27,28 lies in blockGroup3, Cell 26: blockNumber=1, Cell27: blockNumber=2, Cell28: blocknumber=3

// Now coming to another part of the problem. Why are we calculating blockGroup and blockNumber ??
// ** Here, the cube coordinate system comes **
// If you see the figure shared: There is pattern in making there.
// Depending on blockGroup, we see a coordinate changing going parallel to the axis.
// eg: Lets suppose cell no. 10:
//                    As, it lies in blockGroup 2, we see all the cells are parallel to y axis (same can be infered for blockGroup 5)
//                    And going from -ve side to +ve side of y axis, y-coordinate is incrementing just like block number.
//                    Cell no. 10: blockNumber 1, y-coordinate is 1
//                    Cell no. 11: blockNumber 2, y-coordinate is 2
// eg: Lets suppose cell no. 26:
//                    As, it lies in blockGroup 3, we see all the cells are parallel to z axis (same can be inferred for blockGroup 6)
//                    And going from +ve side to -ve side of z axis, z-coordinate is decrementing just like block number.
//                    Cell no 26: blockNumber 1, z-coordinate is -1
//                    Cell no 27: blockNumber 2, z-coordinate is -2
//                    Cell no 28: blockNumber 3, z-coordinate is -3

// Now the main part of the problem: Why is there any pattern of x,y,z that exists at the first place ??
// Answer is... We have imagined our honeycomb as the part of the cube which is cut through the plane x+y+z=0 diagonally
// Here is the animation: https://bl.ocks.org/patricksurry/0603b407fa0a0071b59366219c67abca

// the cube cordinate system is taken from: https://www.redblobgames.com/grids/hexagons/


// Disclaimer: There are some instances that we have used a variable only ONCE and never used it again.
// The best practise is to not make another variable. But for better understanding for the reader of code, we feel some of those instances are important.  

// lets calculate the levelOfSpiral and positionInCircle of any point
function calcCoordinates (a) {
    let levelOfSpiral=0;
    let positionInCircle=0;
    
    // levelOfSpiral can be calculated by using equation 3*n*(n-1)+1 which gives the maximum level of spiral position
    // another brilliant example of using while loop (after database class)
    while (3*levelOfSpiral*(levelOfSpiral-1)+1 < a){
        levelOfSpiral++;
    }

    // positionInCircle = a - (no. of combs till preultimate level)
    let preSpiralLastCell;
    // substituting n as n-1 in equation to find max combs till preultimate level 
    preSpiralLastCell = 3*(levelOfSpiral-1)*(levelOfSpiral-2) + 1;
    positionInCircle = a==1 ? 0 : a - preSpiralLastCell;
    
    // finding x,y,z of cubical coordinate system using positionInCircle and levelOfSpiral
    let x,y,z =0;
    // let totalBlocksInSpiral = (levelOfSpiral-1)*6;
    let cellsEachGroup = levelOfSpiral-1;
    let blockGroup = Math.ceil(positionInCircle/cellsEachGroup);
    let blockNumber =  positionInCircle%cellsEachGroup!=0 ? positionInCircle%cellsEachGroup : cellsEachGroup;

    // these are the patterns we observe in every blockGroup
    if(blockGroup == 1){
        z= levelOfSpiral-1;
        x= -blockNumber;
        y= -x-z;
    }
    if(blockGroup == 2){
        x= -1*(levelOfSpiral-1);
        y= blockNumber;
        z= -x-y;
    }
    if(blockGroup == 3){
        y= levelOfSpiral-1;
        z= -blockNumber;
        x= -z-y;
    }
    if(blockGroup == 4){
        z= -1*(levelOfSpiral-1);
        x= blockNumber;
        y= -x-z;
    }
    if(blockGroup == 5){
        x= levelOfSpiral-1;
        y= -blockNumber;
        z= -x-y;
    }
    if(blockGroup == 6){
        y= -1*(levelOfSpiral-1);
        z= blockNumber;
        x= -z-y;
    }
    // return {levelOfSpiral,positionInCircle,cellsEachGroup,blockNumber,blockGroup,x,y,z};
    return [x,y,z];
}

const a =62;
const b =66;

const [x1,y1,z1] = calcCoordinates(a);
const [x2,y2,z2] = calcCoordinates(b)

const distance = (Math.abs(x2-x1)+Math.abs(y2-y1)+Math.abs(z2-z1))/2;

console.log({x1,y1,z1,x2,y2,z2});
console.log(distance);
