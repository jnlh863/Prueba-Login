package com.example.logintest.Responses

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class Token(
    @SerializedName("token") val token : String,
    @SerializedName("id") val id : String
)
