//
//  ISSInYourRoomVOApp.swift
//  ISSInYourRoomVO
//
//  Created by Yasuhito Nagatomo on 2023/07/12.
//

import SwiftUI

@main
struct ISSInYourRoomVOApp: App {
    var body: some Scene {
        WindowGroup {
            ContentView()
        }

        ImmersiveSpace(id: "ImmersiveSpace") {
            ImmersiveView()
        }
    }
}
