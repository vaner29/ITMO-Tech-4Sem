
  import scala.util.chaining._
  import scala.language.implicitConversions
  class ScalaLiba {

    def plus1(i: Int) = i + 1
    def double(i: Int) = i * 2
    def square(i: Int) = i * i
    def pipeSample(i: Int) = i.pipe(plus1).pipe(double).pipe(square)

    def main(args: Array[String]) = {
      // println(pipeSample(1))
    }
  }