#!/bin/bash

# Set maximum file size for each commit (in bytes)
MAX_SIZE=500000000 # 500MB

# Set threshold for LFS tracking (in bytes)
LFS_THRESHOLD=80000000 # 80MB

# Ensure Git LFS is installed
git lfs install
git add .

# Function to check the size of staged files
function staged_size {
    git ls-files -s | awk '{sum += $4} END {print sum}'
}

# Function to commit and push changes
function commit_and_push {
    handle_missing_lfs_files
    git commit -m "Batch commit"
    git push -u origin main
    echo "Waiting for 3 minutes before the next operation..."
    sleep 180
    total_size=0
}

# Function to get file size with correct command based on OS
function get_file_size {
    if [[ "$OSTYPE" == "darwin"* ]]; then
        stat -f%z "$1"
    else
        stat -c%s "$1"
    fi
}

# Function to remove missing LFS files and check for their availability
function handle_missing_lfs_files {
    echo "handling"
    missing_lfs_files=$(git lfs ls-files | grep '(missing)' | awk '{print $2}')
    for file in $missing_lfs_files; do
        git rm --cached "$file"
        echo "removed $file"
        if [ -f "$file" ]; then
            echo "Re-adding available LFS file: $file"
            git lfs track "$file"
            git add "$file"
        fi
    done
}

# Handle missing LFS files
handle_missing_lfs_files

# Check for and add available LFS files
git lfs ls-files | while read -r line; do
    oid=$(echo "$line" | cut -d' ' -f1)
    file=$(echo "$line" | cut -d' ' -f2)
    
    if [ -f "$file" ]; then
        git lfs track "$file"
        git add "$file"
        echo "Added available LFS file: $file"
    fi
done

# Push all LFS objects explicitly
git lfs push --all origin

# Initialize total size
total_size=0

# ... [previous script content]

# Main loop - process new and modified files
git status -s | while read -r line; do
    # Extract the status and filename
    status=${line:0:2}
    file=${line:3}

    # Process only new ('??') or modified ('M') files
    if [[ "$status" == "??" || "$status" == " M" || "$status" == "M " ]]; then
        if [ -f "$file" ]; then
            file_size=$(get_file_size "$file")
            new_size=$((total_size + file_size))

            # Check if adding this file exceeds the MAX_SIZE
            if [ $new_size -gt $MAX_SIZE ]; then
                git add "${batch_files[@]}"
                commit_and_push
                batch_files=()
            fi

            # Add file to batch
            batch_files+=("$file")
            total_size=$new_size
        fi
    fi
done

# ... [rest of the script]

# Commit and push any remaining files
if [ ${#batch_files[@]} -gt 0 ]; then
    git add "${batch_files[@]}"
    commit_and_push
fi

echo "All files have been pushed."
