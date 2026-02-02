// Copyright 2018 Trinkler Software AG (CH).
// Trinkler Software provides free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version <http://www.gnu.org/licenses/>.

// **************************************************
// View HTML template.
// **************************************************
<template>
    <div id="accessfileApplication">

        <!-- ---------------------------------------------- -->
        <!-- Header                                         -->
        <!-- ---------------------------------------------- -->
        <b-navbar toggleable="md" type="dark" variant="dark">
            <b-navbar-brand href="#">Accessfile v{{ AF.VERSION }}</b-navbar-brand>
            <b-navbar-nav class="ml-auto">
                <b-form-radio-group buttons v-model="action" :options="actions"/>
            </b-navbar-nav>
        </b-navbar>
        <br />

        <!-- ---------------------------------------------- -->
        <!-- Container - Generate                            -->
        <!-- ---------------------------------------------- -->
        <b-container v-show="action === 'create'">
            <!-- ---------------------------------------------- -->
            <!-- Credentials                                    -->
            <!-- ---------------------------------------------- -->
            <b-form-group>
                <b-row>
                    <b-col sm="3">
                        <label for="passwordInput">Password:</label>
                    </b-col>
                    <b-col sm="9">
                        <b-form-input
                            v-model="generated.credentials.password"
                            id="passwordInput"
                            type="password"
                            required
                            placeholder="Please enter a strong password" />
                    </b-col>
                </b-row>
                <br />
                <b-button v-on:click="onGenerateFromPassword" variant="secondary" :block=true>Create Accessfile</b-button>
                <br />
            </b-form-group>

            <div id="accessfileContainer" class="d-block text-center" />
        </b-container>

        <!-- ---------------------------------------------- -->
        <!-- Modal - displays generated wallet              -->
        <!-- ---------------------------------------------- -->
        <b-modal ref="afModalRef" hide-footer title="Accessfile">
            <b-form-group>
                <b-row>
                    <b-col sm="2">
                        <label for="filenameInput">Filename:</label>
                    </b-col>
                    <b-col sm="10">
                        <b-form-input
                            v-model="generated.filename"
                            id="filenameInput"
                            type="text"
                            required
                            placeholder="Please enter your wallet's file name" />
                    </b-col>
                </b-row>
            </b-form-group>
            <b-btn class="mt-3" block @click="onSaveWallet">SAVE</b-btn>
        </b-modal>

        <!-- ---------------------------------------------- -->
        <!-- Container - Decrypt                            -->
        <!-- ---------------------------------------------- -->
        <b-container v-show="action === 'decrypt'">
            <b-form-group>
                <b-row>
                    <b-col sm="3">
                        <label for="decryptionPasswordInput">Password:</label>
                    </b-col>
                    <b-col sm="9">
                        <b-form-input
                            id="decryptionPasswordInput"
                            type="password"
                            v-model="decrypted.password"
                            required
                            placeholder="Please enter a password" />
                    </b-col>
                </b-row>
            </b-form-group>
            <b-form-group>
                <b-form-file
                    v-model="decrypted.walletFile"
                    :state="Boolean(decrypted.walletFile)"
                    @input=onWalletFileSelected
                    accept="image/png"
                    placeholder="Choose an Accessfile ..." />
            </b-form-group>
            <b-form-group v-show="decrypted.walletFile">
                <b-row>
                    <b-col sm="3">
                        <label for="decryptedSecretSeed">Entropy:</label>
                    </b-col>
                    <b-col sm="9">
                        <b-form-input
                            id="decryptedSecretSeed"
                            type="text"
                            v-model="decrypted.secretSeed"
                            disabled />
                    </b-col>
                </b-row>
            </b-form-group>
            <b-form-group v-show="decrypted.walletFile">
                <b-row>
                    <b-col sm="3">
                        <label for="decryptedMnemonic">Mnemonic:</label>
                    </b-col>
                    <b-col sm="9">
                        <b-form-input
                            id="decryptedMnemonic"
                            type="text"
                            v-model="decrypted.mnemonic"
                            disabled />
                    </b-col>
                </b-row>
            </b-form-group>
            <b-form-group v-show="decrypted.walletFile">
                <b-row>
                    <b-col sm="3">
                        <label for="decryptedWallet">Accessfile:</label>
                    </b-col>
                    <b-col sm="9">
                        <b-img id="decryptedWallet" alt="Decoded Accessfile" fluid style="text-align: center;"/>
                    </b-col>
                </b-row>
            </b-form-group>
        </b-container>

    </div>
</template>

// **************************************************
// View Javascript.
// **************************************************
<script>
import 'babel-polyfill';
import * as AF from '@trinkler/accessfile-core';
import * as Accessfile from '../src/index';
import { saveAs } from 'file-saver';
import domtoimage from 'dom-to-image';

// Application view.
export default {
    name: 'accessfileApplication',

    // View model.
    data() {
        return {
            action: 'create',
            actions: [
                { value: 'create', text: 'Create' },
                { value: 'decrypt', text: 'Decrypt' },
            ],
            decrypted: {
                password: 'P96P4Bdp6wMy4pBV',
                secretSeed: null,
                walletFile: null,
                walletImage: null
            },
            generated: {
                credentials: {
                    password: 'P96P4Bdp6wMy4pBV',
                },
                filename: 'my-blockchain-accessfile',
                wallet: null,
            },
            AF
        };
    },

    // View handlers.
    methods: {
        // Event handler: on generation from password.
        onGenerateFromPassword(evt) {
            Accessfile.create(this.generated.credentials.password, 0, {})
                .then(this.onGenerationComplete)
                .catch(this.onGenerationError);
        },

        // Event handler: on generation complete.
        async onGenerationComplete(wallet) {
            // Cache result.
            this.generated.wallet = wallet.$canvas;

            // Update DOM.
            const $container = document.getElementById(
                'accessfileContainer',
            );
            while ($container.firstChild) {
                $container.removeChild($container.firstChild);
            }
            $container.appendChild(wallet.$canvas);

            // Display wallet modal.
            this.$refs.afModalRef.show();
        },

        // Event handler: on generation error.
        async onGenerationError(err) {
            console.error(err);
            alert(err.message);
        },

        // Event handler: on selection of a wallet file.
        async onWalletFileSelected(walletFileBlob) {
            await Accessfile.decrypt(walletFileBlob, this.decrypted.password)
                .then(this.onDecryptionComplete)
                .catch(this.onDecryptionError);
        },

        // Event handler: on decryption completed.
        onDecryptionComplete(response) {
            this.decrypted.secretSeed = response.seed.toString('hex');
            const reader = new FileReader();
            reader.onload = function(e) {
                document.getElementById('decryptedWallet').src = reader.result;
            };
            reader.readAsDataURL(this.decrypted.walletFile);
        },

        // Event handler: on decryption error.
        onDecryptionError(err) {
            this.decrypted.secretSeed = '';
            console.error(err);
            alert(err.message);
        },

        // Event handler: on save wallet to file system.
        onSaveWallet(evt) {
            domtoimage.toBlob(this.generated.wallet).then((blob) => {
                saveAs(blob, `${this.generated.filename}.png`);
            });
        },
    },
};
</script>
