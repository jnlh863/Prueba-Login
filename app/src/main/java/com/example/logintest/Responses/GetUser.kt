package com.example.logintest.Responses

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class GetUser(
    @SerializedName("id") val id : String,
    @SerializedName("username") val username: String,
    @SerializedName("email") val email: String
)
