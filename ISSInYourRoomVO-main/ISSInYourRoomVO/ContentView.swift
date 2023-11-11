//
//  ContentView.swift
//  ISSInYourRoomVO
//
//  Created by Yasuhito Nagatomo on 2023/07/12.
//

import SwiftUI
import RealityKit
import RealityKitContent

struct ContentView: View {

    @State var showImmersiveSpace = false

    @Environment(\.openImmersiveSpace) var openImmersiveSpace
    @Environment(\.dismissImmersiveSpace) var dismissImmersiveSpace

    var body: some View {
        NavigationSplitView {
            List {
                Text("International Space Station")
                Text("ICESat")
                Text("InSight Cruise Stage")
            }
            .navigationTitle("NASA")
        } detail: {
            VStack {
                HStack {
                    Image("iss_428x321") // Picture: (C)NASA
                    Text("The International Space Station (ISS) is the largest modular space station in low Earth orbit. The project involves five space agencies: the United States' NASA, Russia's Roscosmos, Japan's JAXA, Europe's ESA, and Canada's CSA. The ownership and use of the space station is established by intergovernmental treaties and agreements.\n3D Model: (c)NASA, Johnson Space Center Integrated Graphics, Operations, and Analysis Laboratory.(IGOAL)") // Text: (C)Wikipedia
                        .fixedSize(horizontal: false, vertical: true)
                }
                .padding()

                Toggle("Show ImmersiveSpace", isOn: $showImmersiveSpace)
                    .toggleStyle(.button)
                    .padding(.top, 50)
            }
            .navigationTitle("International Space Station")
            .padding()
        }
        .onChange(of: showImmersiveSpace) { _, newValue in
            Task {
                if newValue {
                    await openImmersiveSpace(id: "ImmersiveSpace")
                } else {
                    await dismissImmersiveSpace()
                }
            }
        }
    }
}

#Preview {
    ContentView()
}
