package com.example.logintest.Responses

import com.google.gson.annotations.SerializedName

data class GetUserResponse(
    @SerializedName("mensaje") val mensaje: String,
    @SerializedName("response") val response: GetUser
)
