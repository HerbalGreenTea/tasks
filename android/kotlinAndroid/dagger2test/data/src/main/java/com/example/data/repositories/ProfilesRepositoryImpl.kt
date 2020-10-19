package com.example.data.repositories

import com.example.domain.useCase.ProfilesRepository
import javax.inject.Inject

class ProfilesRepositoryImpl: ProfilesRepository {

    @Inject
    constructor() {

    }

    override suspend fun loadProfile(str: String): String {
        return ("test " + str)
    }
}