module TestUtils

open System
open System.Numerics
open System.Text.RegularExpressions

let generateValuesBlock count wrap ( nextFn : unit -> string ) =
    Array.init count ( fun _ -> nextFn () )
    |> Array.chunkBySize wrap
    |> Array.map ( fun lineValues -> sprintf "%7s: %s" "Values" ( lineValues |> String.concat " " ) )
    |> String.concat "\n"

// Produce output similar to pcg-c test suite output files (with description removed, line breaks within value lists removed)
let generateOutput bits len ( nextFn : unit -> string ) ( nextBoundedFn : int -> int ) =

    let values = Array.init len ( fun _ -> nextFn () ) |> String.concat " "

    let getCoin value =
        if value = 1 then 'H'
        elif value = 0 then 'T'
        else 'X' // bounds failure
    let coins = Array.init 65 ( fun _ -> nextBoundedFn 2 |> getCoin )

    let rolls = Array.init 33 ( fun _ -> nextBoundedFn 6 + 1 )

    let cards = Array.init 52 id
    for i in 52 .. -1 .. 2 do
        let chosen = nextBoundedFn i
        let card = cards.[ chosen ]
        cards.[ chosen ] <- cards.[ i - 1 ]
        cards.[ i - 1 ] <- card
    let ranks = "A23456789TJQK"
    let suits = "hcds"
    let cardNames = cards |> Array.map ( fun n -> sprintf "%c%c" ranks.[ n / 4 ] suits.[ n % 4 ] )

    let outStr =
        [
            sprintf "%7s: %s" ( string bits + "bit" ) values
            sprintf "%7s: %s" "Coins" ( String( coins ) )
            sprintf "%7s: %s" "Rolls" ( rolls |> Array.map string |> String.concat " " )
            sprintf "%7s: %s" "Cards" ( cardNames |> String.concat " " )
        ]
        |> String.concat "\n"
    outStr

let PCG_128BIT_CONSTANT ( hi : uint64 ) ( lo : uint64 ) = ( bigint hi <<< 64 ) + bigint lo
let bigintMaxUInt64 = bigint UInt64.MaxValue
let bigintMaxUInt128 = PCG_128BIT_CONSTANT UInt64.MaxValue UInt64.MaxValue
let bigintToUInt64 ( value : bigint ) = value &&& bigintMaxUInt64 |> uint64
let bigintToHiLoUInt64 value = bigintToUInt64 ( value >>> 64 ) , bigintToUInt64 value
let bigintToHex value =
    let hi , lo = bigintToHiLoUInt64 value
    "0x" + ( sprintf "%x" ( hi ) ).PadLeft( 64 / 4 , '0' ) + ( sprintf "%x" ( lo ) ).PadLeft( 64 / 4 , '0' )


let referenceSeed8 = 42uy
let referenceStream8 = 54uy

let referenceSeed16 = 42us
let referenceStream16 = 54us

let referenceSeed32 = 42u
let referenceStream32 = 54u

let referenceSeed64 = 42UL
let referenceStream64 = 54UL

let referenceSeed128 = 42I
let referenceStream128 = 54I


let maxSeed8 = Byte.MaxValue
let maxStream8 = maxSeed8

let maxSeed16 = UInt16.MaxValue
let maxStream16 = maxSeed16

let maxSeed32 = UInt32.MaxValue
let maxStream32 = maxSeed32

let maxSeed64 = UInt64.MaxValue
let maxStream64 = maxSeed64

let maxSeed128 = bigintMaxUInt128
let maxStream128 = maxSeed128



let getExpectedResultBlock ( testResultData : string ) variantStr =
    let linePrefix = "check-" + variantStr + ".out:"
    Regex.Split( testResultData.Trim() , @"\r?\n|\r" )
    |> Array.filter ( fun line -> line.StartsWith( linePrefix ) )
    |> Array.map ( fun line -> line.Substring( line.IndexOf( ":" ) + 1 ) )
    |> String.concat "\n"

let getValueCountAndWrap ( outputBlock : string ) =
    let lines = Regex.Split( outputBlock , @"\r?\n|\r" )
    let line = lines.[ 0 ]
    let startPos = line.IndexOf( ": 0x" )
    let values = line.Substring( startPos + 2 ).Split [| ' ' |]
    values.Length * lines.Length , values.Length

let getExpectedValueCount ( outputBlock : string ) =
    let line =
        Regex.Split( outputBlock , @"\r?\n|\r" )
        |> Array.find ( fun line -> line.Contains( "bit: 0x" ) )
    let startPos = line.IndexOf( "bit: 0x" )
    let values = line.Substring( startPos + 5 ).Split [| ' ' |]
    let len = values |> Array.length
    let bits = ( values.[ 0 ].Length - 2 ) * 4
    bits , len

