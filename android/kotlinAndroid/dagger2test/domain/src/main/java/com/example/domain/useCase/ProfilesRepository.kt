package com.example.domain.useCase

import androidx.appcompat.widget.LinearLayoutCompat
import dagger.Component
import dagger.Module
import dagger.Provides
import javax.inject.Inject

interface ProfilesRepository {

    suspend fun loadProfile(str: String): String
}

class LoadProfileUseCase {

    lateinit var profilesRepository: ProfilesRepository

    @Inject
    constructor(profilesRepository: ProfilesRepository) {
        this.profilesRepository = profilesRepository
    }

    suspend fun loadProfile(): String {
        return profilesRepository.loadProfile("test")
    }

    fun Start(): String {
        return "startDagger"
    }
}