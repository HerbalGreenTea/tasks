package com.example.dagger2test.ui

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.dagger2test.R
import com.example.dagger2test.YourApp
import com.example.domain.useCase.LoadProfileUseCase
import kotlinx.android.synthetic.main.fragment_blank.*

class BlankFragment : Fragment() {

    lateinit var loadProfileUseCase: LoadProfileUseCase

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        loadProfileUseCase = (requireActivity().application as YourApp)
            .applic.getLoadProfileUseCase()
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_blank, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        name.text = loadProfileUseCase.Start()
    }
}