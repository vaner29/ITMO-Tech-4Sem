module FarshLibary

type Shape =
| Circle of float
| EquilateralTriangle of double
| Square of double
| Rectangle of double * double
let pi = 3.141592654

let area myShape =
    match myShape with
    | Circle radius -> pi * radius * radius
    | EquilateralTriangle s -> (sqrt 3.0) / 4.0 * s * s
    | Square s -> s * s
    | Rectangle (h, w) -> h * w
