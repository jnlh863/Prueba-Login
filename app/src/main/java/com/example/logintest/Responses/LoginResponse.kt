package com.example.logintest.Responses

import com.google.gson.annotations.SerializedName

data class LoginResponse(
    @SerializedName("mensaje") val mensaje: String,
    @SerializedName("response") val response: Token
)
