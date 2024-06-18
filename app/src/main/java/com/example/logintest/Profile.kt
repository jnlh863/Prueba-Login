package com.example.logintest

import android.content.Context.MODE_PRIVATE
import android.content.Intent
import android.icu.text.SimpleDateFormat
import android.icu.util.Calendar
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import com.example.logintest.Responses.CreateProfile
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.util.Date

class Profile : Fragment() {

    var PROF = CreateProfile("", "", 0, 0, "")
    var band = true;
    val preferences = activity?.getSharedPreferences("Preferences", MODE_PRIVATE)
    private lateinit var iU: TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_profile, container, false)

        iU = view.findViewById<TextView>(R.id.info_user)

        return view
    }

    /*override fun onResume() {
        super.onResume()
        val userId = preferences?.getString("id", "") ?: ""
        if (userId.isNotEmpty()) {
            infoUser(userId)
        }
    }*/

    override fun onViewCreated(view: View, savedInstanceState: Bundle?){
        super.onViewCreated(view, savedInstanceState)

        var edittextsexo = view.findViewById<EditText>(R.id.sex)
        var edittextestatura = view.findViewById<EditText>(R.id.stature)
        var edittextpeso = view.findViewById<EditText>(R.id.weight)
        var edittextprotocolo = view.findViewById<EditText>(R.id.protocol)
        var btnUpdate = view.findViewById<Button>(R.id.updatebtn)


        btnUpdate.setOnClickListener{
            val id = preferences?.getString("id", "") ?: ""
            val sexo = edittextsexo.text.toString()
            val estatura = edittextestatura.text.toString().toIntOrNull() ?: 0
            val peso = edittextpeso.text.toString().toIntOrNull() ?: 0
            val protocolo = edittextprotocolo.text.toString()
            val validProtocols = arrayOf("Diabetes", "Obesidad")

            if (sexo.isEmpty()){
                Toast.makeText(
                    requireContext(),
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (sexo != "Masculino" || sexo!= "Femenino"){
                Toast.makeText(
                    requireContext(),
                    "Sexo inválido. Solo se permiten 'Masculino' o 'Femenino'",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (estatura == null){
                Toast.makeText(
                    requireContext(),
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (peso == null){
                Toast.makeText(
                    requireContext(),
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (!validProtocols.contains(protocolo)){
                Toast.makeText(
                    requireContext(),
                    "Protocolo inválido. Debe ser uno de los siguientes: ${validProtocols.joinToString(", ")}",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (id.isEmpty()){
                Toast.makeText(
                    requireContext(),
                    "Hubo un problema en guardar la informacion",
                    Toast.LENGTH_SHORT
                ).show();
            }

            createProfile(id, sexo, estatura, peso, protocolo)

        }

    }

    /*private fun infoUser(userId: String) {

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val call = RetrofitClient.webService.getUser(userId)
                val response = call.body()

                if (response != null) {
                    iU.text = "ID: ${response.id} \n\nUsername: ${response.username}\n\nEmail: ${response.email}"
                } else {
                    Toast.makeText(
                        requireContext(),
                        "User not found. Try again",
                        Toast.LENGTH_SHORT
                    ).show()
                    iU.text = "Hubo un error aqui"
                }

            }
        }
    }*/

    private fun createProfile(id: String, sexo : String, estatura : Int, peso : Int, protocolo : String) {
        this.PROF.id = id
        this.PROF.sex = sexo
        this.PROF.stature = estatura
        this.PROF.weight = peso
        this.PROF.protocol = protocolo

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val call = RetrofitClient.webService.createProfile(id, PROF)
                val response = call.body()

                if (response.toString() == "Cambios guardados") {

                    Toast.makeText(requireContext(), "Cambios guardados", Toast.LENGTH_SHORT).show()

                    val intent = Intent(requireContext(), Profile::class.java)
                    startActivity(intent)

                } else {
                    Toast.makeText(
                        requireContext(),
                        "User not found. Try again",
                        Toast.LENGTH_SHORT
                    ).show()
                }

            }
        }

    }
}