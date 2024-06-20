package com.example.logintest.Responses

import java.util.UUID

data class CreateProfile(
    var id : String,
    var sex : String,
    var stature : Int,
    var weight : Int,
    var protocol : String
)
