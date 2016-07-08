﻿#load "load-references.fsx"

#time

module ServerTests =

    open System

    open GenUnitApp
    open GenUnitApp.Utils

    open NUnit.Framework
    open FsUnit

    open Suave
    open Suave.Http
    open Suave.Testing


    [<Test>]
    let ``app routing should return GenUnitApp when get hello`` () =
        let response =
            runWith defaultConfig Server.app
            |> req HttpMethod.GET "/hello" None
        response |> should equal "GenUnitApp"

    [<Test>]
    let ``app routing should return nothing there when get foo`` () =
        let response =
            runWith defaultConfig Server.app
            |> req HttpMethod.GET "/foo" None
        response |> should equal Server.NOT_FOUND_RESPONSE


    [<Test>]
    let ``app routing should return index file when get root`` () =
        let home = 
            __SOURCE_DIRECTORY__
//            AppDomain.CurrentDomain.BaseDirectory
            |> Path.parentDirectory
            |> Path.parentDirectory
            |> Path.parentDirectory
//            |> Path.parentDirectory
            |> Path.combineWith "client"
        let config = Server.getConfig home "8083"
        let response = 
            runWith config Server.app
            |> req HttpMethod.GET "/" None
        response |> should equal "index.html"