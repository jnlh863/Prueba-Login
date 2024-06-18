package com.example.logintest

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.logintest.Responses.CreateUser
import com.example.logintest.Responses.RestPass
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class ResetPass : AppCompatActivity(){

    lateinit var send: Button
    lateinit var correo: EditText
    var RESET = RestPass("")
    var band = true;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.resetpassword)

        correo = findViewById(R.id.email_input)
        send = findViewById(R.id.send_btn)

        send.setOnClickListener(View.OnClickListener {
            val email = correo.getText().toString().trim { it <= ' ' }

            if (email.isEmpty()){
                Toast.makeText(
                    this@ResetPass,
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            sendEmail(email)

        })

    }

    private fun sendEmail(email: String) {
        this.RESET.email = email

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val call = RetrofitClient.webService.restPass(RESET)
                val response = call.body()

                runOnUiThread {
                    if (response.toString() == "The email sends in your bandage") {

                        Toast.makeText(this@ResetPass, "The email sends in your bandage", Toast.LENGTH_SHORT).show()
                        limpiarObjeto()

                        val intent = Intent(this@ResetPass, MainActivity::class.java)
                        startActivity(intent)

                        limpiarcampos()

                        finish()

                    } else {
                        Toast.makeText(
                            this@ResetPass,
                            "Error de al enviar el correo",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }
            }
        }

    }
    private fun limpiarcampos() {
        correo.setText("")
    }

    private fun limpiarObjeto() {
        this.RESET.email = ""
    }







}