package com.example.logintest.Responses

import com.google.gson.annotations.SerializedName

data class GetProfileResponse(
    @SerializedName("mensaje") val mensaje: String,
    @SerializedName("response") val response: CreateProfile
)
